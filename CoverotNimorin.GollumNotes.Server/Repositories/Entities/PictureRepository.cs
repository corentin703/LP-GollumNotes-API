using CoverotNimorin.GollumNotes.Server.Contexts;
using CoverotNimorin.GollumNotes.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumNotes.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoverotNimorin.GollumNotes.Server.Repositories.Entities;

public class PictureRepository : BaseEntityRepository<Picture>, IPictureRepository
{
    public PictureRepository(GollumNotesContext dbContext) : base(dbContext)
    {
        //
    }

    public async Task<List<Picture>> GetAllByNoteAsync(Note note)
    {
        return await DbSet
            .Where(
                picture => picture.NoteId == note.Id
            )
            .ToListAsync();
    }
}