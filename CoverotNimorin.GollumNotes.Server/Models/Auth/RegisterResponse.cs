using CoverotNimorin.GollumNotes.Server.Entities;

namespace CoverotNimorin.GollumNotes.Server.Models.Auth;

public class RegisterResponse
{
    public string Id { get; set; }
    public string Username { get; set; }
    
    public RegisterResponse(User user)
    {
        Id = user.Id;
        Username = user.Username;
    }
}