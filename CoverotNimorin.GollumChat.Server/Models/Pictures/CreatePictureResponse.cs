using CoverotNimorin.GollumChat.Server.Entities;

namespace CoverotNimorin.GollumChat.Server.Models.Pictures;

public class CreatePictureResponse
{
    public string Id { get; set; }

    public CreatePictureResponse(Picture picture)
    {
        Id = picture.Id;
    }
}