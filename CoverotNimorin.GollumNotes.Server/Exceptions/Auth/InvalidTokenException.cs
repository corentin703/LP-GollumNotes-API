using System.Net;

namespace CoverotNimorin.GollumNotes.Server.Exceptions.Auth;

public class InvalidTokenException : WebException
{
    public InvalidTokenException() : base(HttpStatusCode.Unauthorized, "Jeton invalide")
    {
        //
    }
}