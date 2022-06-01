using System.Net;

namespace CoverotNimorin.GollumChat.Server.Exceptions;

public class EntityNotFoundException : WebException
{
    public EntityNotFoundException(string id) 
        : base(HttpStatusCode.NotFound, $"Ressource introuvable (id: {id})")
    {
        //
    }
}