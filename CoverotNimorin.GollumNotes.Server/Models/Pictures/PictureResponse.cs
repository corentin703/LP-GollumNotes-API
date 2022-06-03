using CoverotNimorin.GollumNotes.Server.Entities;

namespace CoverotNimorin.GollumNotes.Server.Models.Pictures;

public class PictureResponse
{
    public string Id { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public PictureResponse()
    {
        //
    }

    public PictureResponse(Picture picture)
    {
        Id = picture.Id;
        ContentType = picture.ContentType;
        CreatedAt = picture.CreatedAt.ToLocalTime();
    }
}