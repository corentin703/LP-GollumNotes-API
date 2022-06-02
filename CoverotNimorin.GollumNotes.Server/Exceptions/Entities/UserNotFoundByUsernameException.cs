using System.Net;
using WebException = CoverotNimorin.GollumNotes.Server.Exceptions.WebException;

namespace CoverotNimorin.GollumNotes.Server.Exceptions.Entities;

public class UserNotFoundByUsernameException : WebException
{
    public UserNotFoundByUsernameException(string username) 
        : base(HttpStatusCode.NotFound, $"Utilisateur introuvable (nom: {username})")
    {
        //
    }
}