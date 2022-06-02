using CoverotNimorin.GollumNotes.Server.Contracts.Services;
using CoverotNimorin.GollumNotes.Server.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CoverotNimorin.GollumNotes.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest model)
    {
        LoginResponse? response = await _authService.LoginAsync(model);
        if (response == null)
            return BadRequest("Identifiants invalides");

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest model)
    {
        RegisterResponse response = await _authService.RegisterAsync(model);
        return Ok(response);
    }
}