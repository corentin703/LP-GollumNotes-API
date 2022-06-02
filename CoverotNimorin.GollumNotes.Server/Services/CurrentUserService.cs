using CoverotNimorin.GollumNotes.Server.Constants;
using CoverotNimorin.GollumNotes.Server.Contracts.Services;
using CoverotNimorin.GollumNotes.Server.Entities;
using CoverotNimorin.GollumNotes.Server.Exceptions.Auth;

namespace CoverotNimorin.GollumNotes.Server.Services;

public class CurrentUserService : ICurrentUserService
{
    public User? CurrentUser { get; }

    public CurrentUserService(IHttpContextAccessor httpContext)
    {
        CurrentUser = httpContext.HttpContext?.Items[AuthConstants.HttpContextCurrentUser] as User;
    }
    
    public User GetRequiredUser()
    {
        if (CurrentUser == null)
            throw new NotLoggedInException();

        return CurrentUser;
    }
}