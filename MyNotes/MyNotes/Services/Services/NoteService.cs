using MyNotes.Data.Repositories.IRepositories;
using MyNotes.Models;
using MyNotes.Services.IServices;

namespace MyNotes.Services.Services;

public class NoteService : INoteService
{

    private readonly INotesRepository _notesRepository;

    public NoteService(INotesRepository notesRepository)
    {
        _notesRepository = notesRepository;
    }
    
    
    public async Task<Notes> AddNotes(Notes notes)
    {
        var note = new Notes()
        {
            Title = notes.Title,
            Note = notes.Note
        };

        await _notesRepository.AddNotes(note);
        return note;
    }

    public async Task<Notes> Update(Notes notes)
    {
        var oldnote = await _notesRepository.GetById(notes.Id);
        if (oldnote == null) throw new Exception("Notes is null!");

        oldnote.Title = notes.Title;
        oldnote.Note = notes.Note;
        await _notesRepository.Update(oldnote);
        return oldnote;

    }

    public async Task<Notes> Delete(Notes notes)
    {
        if (notes == null) throw new Exception("Note is null!");
        return await _notesRepository.Delete(notes);
    }

    public async Task<List<Notes>> GetAll()
    {
        var notes = await _notesRepository.GetAll();
        if (notes.Any())
        {
            return notes.Select(a => new Notes()
            {
                Id = a.Id,
                IsCreate = a.IsCreate,
                Title = a.Title,
                Note = a.Note
            }).ToList();
        }

        return new List<Notes>();
    }

    public async Task<Notes> GetById(long Id)
    {
        if (Id <= 0) throw new Exception("Id is not 0 low!");
        return await _notesRepository.GetById(Id);
    }
}