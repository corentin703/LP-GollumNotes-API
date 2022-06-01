using CoverotNimorin.GollumChat.Server.Entities;

namespace CoverotNimorin.GollumChat.Server.Contracts;

public interface IUserService
{ 
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(string id);
    Task<User?> GetByUsernameAsync(string username);

    Task AddUserAsync(User user);
}