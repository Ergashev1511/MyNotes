using MyNotes.Models;

namespace MyNotes.Data.Repositories.IRepositories;

public interface IUserRepository
{
    Task<User> AddUser(User user);
    Task<User> LoginByPin(string pin);
    Task<User> UpdateUser(User user);
    Task<User> GetById(long Id);
    Task<List<User>> GetAllUser();
}