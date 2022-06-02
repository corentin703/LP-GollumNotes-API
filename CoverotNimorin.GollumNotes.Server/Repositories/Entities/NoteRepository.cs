using CoverotNimorin.GollumNotes.Server.Contexts;
using CoverotNimorin.GollumNotes.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumNotes.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoverotNimorin.GollumNotes.Server.Repositories.Entities;

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