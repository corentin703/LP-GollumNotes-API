using CoverotNimorin.GollumNotes.Server.Entities;

namespace CoverotNimorin.GollumNotes.Server.Contracts.Repositories.Entities;

public interface INoteRepository : IEntityRepository<Note>
{
    Task<List<Note>> GetAllByUserAsync(string userId);
}