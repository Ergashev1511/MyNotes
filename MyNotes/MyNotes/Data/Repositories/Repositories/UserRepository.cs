using Microsoft.EntityFrameworkCore;
using MyNotes.Data.Repositories.IRepositories;
using MyNotes.Models;

namespace MyNotes.Data.Repositories.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> AddUser(User user)
    {
        if (user == null) throw new Exception("User cannot be null!");
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> LoginByPin(string pin)
    {
        var user = await _context.Users.FirstOrDefaultAsync(a => a.Password == pin);
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetById(long Id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == Id);
        return user;
    }

    public async Task<List<User>> GetAllUser()
    {
        return await _context.Users.Where(a => a.Id > 0).ToListAsync();
    }
}