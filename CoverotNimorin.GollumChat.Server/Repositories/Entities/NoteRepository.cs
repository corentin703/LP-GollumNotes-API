using CoverotNimorin.GollumChat.Server.Contexts;
using CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumChat.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoverotNimorin.GollumChat.Server.Repositories.Entities;

public class NoteRepository : BaseEntityRepository<Note>, INoteRepository
{
    public NoteRepository(GollumNotesContext dbContext) : base(dbContext)
    {
        //
    }

    public async Task<List<Note>> GetAllByUserAsync(string userId)
    {
        return await DbSet
            .Where(
                note => note.UserId == userId
            )
            .ToListAsync();
    }
}