using System.Net;
using WebException = CoverotNimorin.GollumNotes.Server.Exceptions.WebException;

namespace CoverotNimorin.GollumNotes.Server.Exceptions.Entities;

public class EntityNotFoundException : WebException
{
    public EntityNotFoundException(string id) 
        : base(HttpStatusCode.NotFound, $"Ressource introuvable (id: {id})")
    {
        //
    }
}