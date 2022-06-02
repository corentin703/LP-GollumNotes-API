using System.Net;
using WebException = CoverotNimorin.GollumNotes.Server.Exceptions.WebException;

namespace CoverotNimorin.GollumNotes.Server.Exceptions.Auth;

public class NotLoggedInException : WebException
{
    public NotLoggedInException() 
        : base(HttpStatusCode.Forbidden, "Vous n'êtes pas connecté")
    {
        //
    }
}