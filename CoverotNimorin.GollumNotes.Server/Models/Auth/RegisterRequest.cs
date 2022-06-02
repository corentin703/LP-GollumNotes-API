using System.ComponentModel.DataAnnotations;

namespace CoverotNimorin.GollumNotes.Server.Models.Auth;

public class RegisterRequest
{
    [Required]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}