using System.Net;

namespace CoverotNimorin.GollumChat.Server.Exceptions;

public class UserNotFoundByUsernameException : WebException
{
    public UserNotFoundByUsernameException(string username) 
        : base(HttpStatusCode.NotFound, $"Utilisateur introuvable (nom: {username})")
    {
        //
    }
}