using CoverotNimorin.GollumChat.Server.Models.Auth;

namespace CoverotNimorin.GollumChat.Server.Contracts;

public interface IAuthService
{
    Task<RegisterResponse?> RegisterAsync(RegisterRequest model);
    Task<LoginResponse?> LoginAsync(LoginRequest model);
}