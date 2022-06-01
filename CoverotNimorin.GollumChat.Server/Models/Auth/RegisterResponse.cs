using CoverotNimorin.GollumChat.Server.Entities;

namespace CoverotNimorin.GollumChat.Server.Models.Auth;

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