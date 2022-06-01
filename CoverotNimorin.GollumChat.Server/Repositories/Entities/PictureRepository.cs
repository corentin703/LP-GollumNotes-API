using CoverotNimorin.GollumChat.Server.Contexts;
using CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumChat.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoverotNimorin.GollumChat.Server.Repositories.Entities;

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