using System.Windows.Documents;
using MyNotes.Models;

namespace MyNotes.Data.Repositories.IRepositories;

public interface INotesRepository
{
    Task<Notes> AddNotes(Notes notes);
    Task<Notes> Update(Notes notes);
    Task<Notes> Delete(Notes notes);
    Task<List<Notes>> GetAll();
    Task<Notes> GetById(long Id);
}