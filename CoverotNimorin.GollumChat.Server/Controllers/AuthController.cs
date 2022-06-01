using CoverotNimorin.GollumChat.Server.Contracts;
using CoverotNimorin.GollumChat.Server.Contracts.Services;
using CoverotNimorin.GollumChat.Server.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CoverotNimorin.GollumChat.Server.Controllers;

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