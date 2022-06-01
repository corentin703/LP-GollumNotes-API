using System.Net;

namespace CoverotNimorin.GollumChat.Server.Exceptions;

public class PictureProcessingException : WebException
{
    public PictureProcessingException() 
        : base(HttpStatusCode.ServiceUnavailable, "Erreur pendant l'enregistrement de l'image")
    {
        //
    }
}