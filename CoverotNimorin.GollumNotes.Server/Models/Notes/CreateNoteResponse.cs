using System.Text.Json.Serialization;
using CoverotNimorin.GollumNotes.Server.Entities;

namespace CoverotNimorin.GollumNotes.Server.Models.Notes;

public class CreateNoteResponse
{
    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; }
    
    [JsonPropertyName("content")] public string Content { get; set; }

    [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }

    public CreateNoteResponse(Note note)
    {
        Id = note.Id;
        Title = note.Title;
        Content = note.Content;
        CreatedAt = note.CreatedAt.ToLocalTime();
    }
}