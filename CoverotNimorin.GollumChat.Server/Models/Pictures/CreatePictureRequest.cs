using System.ComponentModel.DataAnnotations;

namespace CoverotNimorin.GollumChat.Server.Models.Pictures;

public class CreatePictureRequest
{
    [Required] public IFormFile File { get; set; } = default!;
}