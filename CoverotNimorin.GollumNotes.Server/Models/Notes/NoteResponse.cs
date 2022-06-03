using CoverotNimorin.GollumNotes.Server.Entities;
using CoverotNimorin.GollumNotes.Server.Models.Pictures;

namespace CoverotNimorin.GollumNotes.Server.Models.Notes;

public class NoteResponse
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }

    public List<PictureResponse> Pictures { get; set; } = new();

    public NoteResponse()
    {
        //
    }

    public NoteResponse(Note note)
    {
        Id = note.Id;
        Title = note.Title;
        Content = note.Content;
        CreatedAt = note.CreatedAt.ToLocalTime();
        LastModifiedAt = note.LastModifiedAt?.ToLocalTime();
        Pictures = note.Pictures.Select(picture => new PictureResponse(picture)).ToList();
    }
}