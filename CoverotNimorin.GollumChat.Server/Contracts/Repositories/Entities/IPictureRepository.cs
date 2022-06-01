using CoverotNimorin.GollumChat.Server.Entities;

namespace CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;

public interface IPictureRepository : IEntityRepository<Picture>
{
    //
    Task<List<Picture>> GetAllByNoteAsync(Note note);
}