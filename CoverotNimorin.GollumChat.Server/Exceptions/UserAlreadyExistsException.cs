using System.Net;

namespace CoverotNimorin.GollumChat.Server.Exceptions;

public class UserAlreadyExistsException : WebException
{
    public UserAlreadyExistsException() :
        base(HttpStatusCode.BadRequest, "Cet utilisateur existe déjà")
    {
        //
    }
}