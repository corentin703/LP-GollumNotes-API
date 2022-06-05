using System.Text.Json.Serialization;

namespace CoverotNimorin.GollumNotes.Server.Models.Notes;

public class UpdateNoteRequest
{
    [JsonIgnore]
    public string Id { get; set; } = string.Empty;
    
    public string? Title { get; set; }
    public string? Content { get; set; }
}