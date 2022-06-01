using CoverotNimorin.GollumChat.Server.Constants;
using CoverotNimorin.GollumChat.Server.Contracts.Services;
using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Exceptions.Auth;

namespace CoverotNimorin.GollumChat.Server.Services;

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