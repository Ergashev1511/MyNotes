using MyNotes.Models;

namespace MyNotes.Services.IServices;

public interface INoteService
{
    Task<Notes> AddNotes(Notes notes);
    Task<Notes> Update(Notes notes);
    Task<Notes> Delete(Notes notes);
    Task<List<Notes>> GetAll();
    Task<Notes> GetById(long Id);
}