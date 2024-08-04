using System.Windows;
using MyNotes.Data.Repositories.IRepositories;
using MyNotes.Models;
using MyNotes.Services.IServices;

namespace MyNotes.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User> AddUser(User user)
    {
        if (user == null) throw new Exception("User cannot be null!");
            User newuser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password
            };
            await _userRepository.AddUser(newuser);
        
      
        return user;
    }

    public async Task<bool> LoginByPin(string password)
    {
        var newuser = await _userRepository.LoginByPin(password);
        if (newuser != null)
        {
             return newuser.Password == password ? true : false;
        }
        else
        {
            return false;
        }

        return false;
    }

    public async Task<User> UpdateUser(User user)
    {
        var olduser = await _userRepository.GetById(user.Id);
        if (olduser == null) throw new Exception("User is null");

        olduser.FirstName = user.FirstName;
        olduser.LastName = user.LastName;
        olduser.Email = user.Email;
        olduser.PhoneNumber = user.PhoneNumber;
        olduser.Password = user.Password;

        await _userRepository.UpdateUser(olduser);
        return olduser;
    }

    public async Task<List<User>> GetAllUser()
    {
        var user = await _userRepository.GetAllUser();
        if (user.Any())
        {
            return user.Select(a => new User()
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Password = a.Password
            }).ToList();
        }

        return new List<User>();
    }

    public async Task<bool> CheckForEntry()
    {
        var user = await _userRepository.GetAllUser();
        if (user.Count() ==1)
        {
            return true;
        }

        return false;
    }
}