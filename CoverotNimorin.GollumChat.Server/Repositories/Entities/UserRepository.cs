using CoverotNimorin.GollumChat.Server.Contexts;
using CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumChat.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoverotNimorin.GollumChat.Server.Repositories.Entities;

public class UserRepository : BaseEntityRepository<User>, IUserRepository
{
    public UserRepository(GollumNotesContext dbContext) : base(dbContext)
    {
        //
    }
    
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await DbSet.FirstOrDefaultAsync(
            user => user.Username == username
        );
    }
}