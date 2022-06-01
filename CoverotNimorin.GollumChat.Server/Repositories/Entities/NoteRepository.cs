using CoverotNimorin.GollumChat.Server.Contexts;
using CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumChat.Server.Entities;

namespace CoverotNimorin.GollumChat.Server.Repositories.Entities;

public class NoteRepository : BaseEntityRepository<Note>, INoteRepository
{
    public NoteRepository(GollumNotesContext dbContext) : base(dbContext)
    {
        //
    }
}