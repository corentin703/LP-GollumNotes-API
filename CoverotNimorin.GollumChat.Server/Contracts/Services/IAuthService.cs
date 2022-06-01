using CoverotNimorin.GollumChat.Server.Models.Auth;

namespace CoverotNimorin.GollumChat.Server.Contracts.Services;

public interface IAuthService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest model);
    Task<LoginResponse> LoginAsync(LoginRequest model);
}