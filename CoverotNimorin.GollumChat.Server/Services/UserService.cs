using CoverotNimorin.GollumChat.Server.Contexts;
using CoverotNimorin.GollumChat.Server.Contracts;
using CoverotNimorin.GollumChat.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoverotNimorin.GollumChat.Server.Services;

public class UserService : IUserService
{
    private readonly GollumNotesContext _dbContext;

    public UserService(GollumNotesContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(
            user => user.Id == id
        );
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(
            user => user.Username == username
        );
    }

    public async Task AddUserAsync(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }
}