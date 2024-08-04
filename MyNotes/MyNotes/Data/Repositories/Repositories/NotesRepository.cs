using Microsoft.EntityFrameworkCore;
using MyNotes.Data.Repositories.IRepositories;
using MyNotes.Models;

namespace MyNotes.Data.Repositories.Repositories;

public class NotesRepository : INotesRepository
{
    private readonly AppDbContext _context;

    public NotesRepository(AppDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<Notes> AddNotes(Notes notes)
    {
        if (notes == null) throw new Exception("Notes cannot be null!");
        await _context.Notes.AddAsync(notes);
        await _context.SaveChangesAsync();
        return notes;
    }

    public async Task<Notes> Update(Notes notes)
    {
        _context.Notes.Update(notes);
        await _context.SaveChangesAsync();
        return notes;
    }

    public async Task<Notes> Delete(Notes notes)
    {
        var note = await _context.Notes.FirstOrDefaultAsync(a => a.Id == notes.Id);
         _context.Notes.Remove(note);
         await _context.SaveChangesAsync();
         return notes;
    }

    public async Task<List<Notes>> GetAll()
    {
        return await _context.Notes.Where(a => a.Id > 0).ToListAsync();
    }

    public async Task<Notes> GetById(long Id)
    {
     var note= await _context.Notes.FirstOrDefaultAsync(a => a.Id == Id);
     return note != null ? note : new Notes();
    }
}