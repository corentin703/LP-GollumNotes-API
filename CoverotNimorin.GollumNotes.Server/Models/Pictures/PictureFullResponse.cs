using CoverotNimorin.GollumNotes.Server.Entities;

namespace CoverotNimorin.GollumNotes.Server.Models.Pictures;

public class PictureFullResponse : PictureResponse
{
    public byte[] Content { get; set; } = Array.Empty<byte>();

    public PictureFullResponse()
        : base()
    {
        //
    }

    public PictureFullResponse(Picture picture)
        : base(picture)
    {
        Content = picture.Content;
    }
}