using System.Text.Json.Serialization;
using CoverotNimorin.GollumNotes.Server.Entities;

namespace CoverotNimorin.GollumNotes.Server.Models.Pictures;

public class CreatePictureResponse
{
    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("contentType")] public string ContentType { get; set; }

    public CreatePictureResponse(Picture picture)
    {
        Id = picture.Id;
        ContentType = picture.ContentType;
    }
}