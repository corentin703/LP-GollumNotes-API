using System.Net;

namespace CoverotNimorin.GollumChat.Server.Exceptions.Auth;

public class LoginException : WebException
{
    public LoginException() : base(HttpStatusCode.Unauthorized, "Identifiants invalides")
    {
        //
    }
}