using System.Net;

namespace CoverotNimorin.GollumChat.Server.Exceptions;

public class LoginException : WebException
{
    public LoginException() : base(HttpStatusCode.Unauthorized, "Identifiants invalides")
    {
        //
    }
}