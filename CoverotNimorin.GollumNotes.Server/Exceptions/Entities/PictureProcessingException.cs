using System.Net;
using WebException = CoverotNimorin.GollumNotes.Server.Exceptions.WebException;

namespace CoverotNimorin.GollumNotes.Server.Exceptions.Entities;

public class PictureProcessingException : WebException
{
    public PictureProcessingException() 
        : base(HttpStatusCode.ServiceUnavailable, "Erreur pendant l'enregistrement de l'image")
    {
        //
    }
}