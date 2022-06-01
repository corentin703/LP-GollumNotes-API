using CoverotNimorin.GollumChat.Server.Contexts;
using CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CoverotNimorin.GollumChat.Server.Repositories.Entities;

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