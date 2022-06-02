using CoverotNimorin.GollumNotes.Server.Entities;

namespace CoverotNimorin.GollumNotes.Server.Contracts.Repositories.Entities;

public interface IUserRepository : IEntityRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
}