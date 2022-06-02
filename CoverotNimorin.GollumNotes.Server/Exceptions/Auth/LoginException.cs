using System.Net;
using WebException = CoverotNimorin.GollumNotes.Server.Exceptions.WebException;

namespace CoverotNimorin.GollumNotes.Server.Exceptions.Auth;

public class LoginException : WebException
{
    public LoginException() : base(HttpStatusCode.Unauthorized, "Identifiants invalides")
    {
        //
    }
}