using System.Net;

namespace CoverotNimorin.GollumChat.Server.Exceptions.Auth;

public class NotLoggedInException : WebException
{
    public NotLoggedInException() 
        : base(HttpStatusCode.Forbidden, "Vous n'êtes pas connecté")
    {
        //
    }
}