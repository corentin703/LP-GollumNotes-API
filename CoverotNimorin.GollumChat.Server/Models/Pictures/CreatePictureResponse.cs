using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using CoverotNimorin.GollumChat.Server.Entities;

namespace CoverotNimorin.GollumChat.Server.Models.Pictures;

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