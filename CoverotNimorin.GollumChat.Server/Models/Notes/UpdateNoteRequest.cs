using System.ComponentModel.DataAnnotations;

namespace CoverotNimorin.GollumChat.Server.Models.Notes;

public class UpdateNoteRequest
{
    [Required]
    public string Id { get; set; } = string.Empty;
    
    public string? Title { get; set; }
    public string? Content { get; set; }
}