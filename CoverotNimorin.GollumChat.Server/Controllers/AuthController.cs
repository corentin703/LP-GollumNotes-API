using CoverotNimorin.GollumChat.Server.Contracts;
using CoverotNimorin.GollumChat.Server.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CoverotNimorin.GollumChat.Server.Controllers;

[Controller]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest model)
    {
        LoginResponse? response = await _authService.LoginAsync(model);
        if (response == null)
            return BadRequest("Identifiants invalides");

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest model)
    {
        RegisterResponse? response = await _authService.RegisterAsync(model);
        if (response == null)
            return BadRequest("Erreur durant l'inscription");

        return Ok(response);
    }
}