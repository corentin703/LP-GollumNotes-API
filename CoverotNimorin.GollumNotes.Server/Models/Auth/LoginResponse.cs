using CoverotNimorin.GollumNotes.Server.Entities;

namespace CoverotNimorin.GollumNotes.Server.Models.Auth;

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