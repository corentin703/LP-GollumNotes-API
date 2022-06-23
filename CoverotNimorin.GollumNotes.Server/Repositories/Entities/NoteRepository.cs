using CoverotNimorin.GollumNotes.Server.Contexts;
using CoverotNimorin.GollumNotes.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumNotes.Server.Entities;
using CoverotNimorin.GollumNotes.Server.Exceptions.Entities;
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
            .Include(note => note.Pictures)
            .ToListAsync();
    }

    public override Note GetById(string id)
    {
        Note? note = DbSet
            .Where(note => note.Id == id)
            .Include(note => note.Pictures)
            .FirstOrDefault();

        if (note == null)
            throw new EntityNotFoundException(id);

        return note;
    }

    public override async Task<Note> GetByIdAsync(string id)
    {
        Note? note = await DbSet.Where(note => note.Id == id)
            .Include(note => note.Pictures)
            .FirstOrDefaultAsync();
        
        if (note == null)
            throw new EntityNotFoundException(id);

        return note;
    }
}