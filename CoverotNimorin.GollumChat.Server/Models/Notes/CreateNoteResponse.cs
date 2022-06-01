using System.ComponentModel.DataAnnotations;
using CoverotNimorin.GollumChat.Server.Entities;

namespace CoverotNimorin.GollumChat.Server.Models.Notes;

public class CreateNoteResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public CreateNoteResponse(Note note)
    {
        Id = note.Id;
        Title = note.Title;
        Content = note.Content;
        CreatedAt = note.CreatedAt.ToLocalTime();
    }
}