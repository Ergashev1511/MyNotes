using MyNotes.Models;

namespace MyNotes.Services.IServices;

public interface IUserService
{
   Task<User> AddUser(User user);
   Task<bool> LoginByPin(string password);
   Task<User> UpdateUser(User user);
   Task<List<User>> GetAllUser();
   Task<bool> CheckForEntry();
}