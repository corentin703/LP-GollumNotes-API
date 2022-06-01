using System.Net;

namespace CoverotNimorin.GollumChat.Server.Exceptions;

public class NoteNotOwnedByUserException : WebException
{
    public NoteNotOwnedByUserException() 
        : base(HttpStatusCode.Forbidden, "Cette note ne vous appartient pas !")
    {
        //
    }
}