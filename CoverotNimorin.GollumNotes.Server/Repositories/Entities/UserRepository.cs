using CoverotNimorin.GollumNotes.Server.Contexts;
using CoverotNimorin.GollumNotes.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumNotes.Server.Entities;
using CoverotNimorin.GollumNotes.Server.Exceptions.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoverotNimorin.GollumNotes.Server.Repositories.Entities;

public class UserRepository : BaseEntityRepository<User>, IUserRepository
{
    public UserRepository(GollumNotesContext dbContext) : base(dbContext)
    {
        //
    }
    
    public async Task<User> GetByUsernameAsync(string username)
    {
        User? user = await DbSet.FirstOrDefaultAsync(
            user => user.Username == username
        );

        if (user == null)
            throw new UserNotFoundByUsernameException(username);

        return user;
    }
}