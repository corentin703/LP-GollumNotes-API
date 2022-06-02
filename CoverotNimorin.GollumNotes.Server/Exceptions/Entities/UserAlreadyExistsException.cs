using System.Net;
using WebException = CoverotNimorin.GollumNotes.Server.Exceptions.WebException;

namespace CoverotNimorin.GollumNotes.Server.Exceptions.Entities;

public class UserAlreadyExistsException : WebException
{
    public UserAlreadyExistsException() :
        base(HttpStatusCode.BadRequest, "Cet utilisateur existe déjà")
    {
        //
    }
}