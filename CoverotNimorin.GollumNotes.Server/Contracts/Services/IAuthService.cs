using CoverotNimorin.GollumNotes.Server.Models.Auth;

namespace CoverotNimorin.GollumNotes.Server.Contracts.Services;

public interface IAuthService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest model);
    Task<LoginResponse> LoginAsync(LoginRequest model);
}