using CoverotNimorin.GollumChat.Server.Entities;

namespace CoverotNimorin.GollumChat.Server.Models.Auth;

public class LoginResponse
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }

    public LoginResponse(User user, string token)
    {
        Id = user.Id;
        Username = user.Username;
        Token = token;
    }
}