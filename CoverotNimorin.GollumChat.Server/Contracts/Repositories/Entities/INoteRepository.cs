using CoverotNimorin.GollumChat.Server.Entities;

namespace CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;

public interface INoteRepository : IEntityRepository<Note>
{
    Task<List<Note>> GetAllByUserAsync(string userId);
}