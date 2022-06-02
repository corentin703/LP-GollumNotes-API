using System.Net;
using WebException = CoverotNimorin.GollumNotes.Server.Exceptions.WebException;

namespace CoverotNimorin.GollumNotes.Server.Exceptions.Entities;

public class PictureNotOwnedByNoteException : WebException
{
    public PictureNotOwnedByNoteException() 
        : base(HttpStatusCode.BadRequest, "Cette image n'appartient pas à cette note")
    {
        //
    }
}