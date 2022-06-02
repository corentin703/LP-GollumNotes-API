using CoverotNimorin.GollumNotes.Server.Entities;

namespace CoverotNimorin.GollumNotes.Server.Contracts.Repositories.Entities;

public interface IPictureRepository : IEntityRepository<Picture>
{
    //
    Task<List<Picture>> GetAllByNoteAsync(Note note);
}