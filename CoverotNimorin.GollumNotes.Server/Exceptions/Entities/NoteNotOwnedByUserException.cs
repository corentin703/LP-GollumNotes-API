using System.Net;
using WebException = CoverotNimorin.GollumNotes.Server.Exceptions.WebException;

namespace CoverotNimorin.GollumNotes.Server.Exceptions.Entities
{
    public class NoteNotOwnedByUserException : WebException
    {
        public NoteNotOwnedByUserException() 
            : base(HttpStatusCode.Forbidden, "Cette note ne vous appartient pas !")
        {
            //
        }
    }
}