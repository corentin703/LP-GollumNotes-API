using System.ComponentModel.DataAnnotations;

namespace CoverotNimorin.GollumChat.Server.Models.Pictures;

public class CreatePictureRequest
{
    public string NoteId { get; set; } = string.Empty;
    [Required] public IFormFile File { get; set; } = default!;
}