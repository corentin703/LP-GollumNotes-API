using CoverotNimorin.GollumChat.Server.Entities;

namespace CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;

public interface IUserRepository : IEntityRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
}