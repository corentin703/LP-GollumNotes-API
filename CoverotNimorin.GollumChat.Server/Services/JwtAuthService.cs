using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoverotNimorin.GollumChat.Server.Configuration;
using CoverotNimorin.GollumChat.Server.Contracts;
using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Exceptions;
using CoverotNimorin.GollumChat.Server.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoverotNimorin.GollumChat.Server.Services;

public class JwtAuthService : IAuthService
{
    private readonly AppConfiguration _appConfiguration;
    private readonly IUserService _userService;

    public JwtAuthService(IOptions<AppConfiguration> appConfiguration, IUserService userService)
    {
        _appConfiguration = appConfiguration.Value;
        _userService = userService;
    }

    public async Task<RegisterResponse?> RegisterAsync(RegisterRequest model)
    {
        User? user = await _userService.GetByUsernameAsync(model.Username);

        if (user != null)
            throw new UserAlreadyExistsException();

        user = new User()
        {
            Username = model.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
        };

        await _userService.AddUserAsync(user);

        return new RegisterResponse(user);
    }
    
    public async Task<LoginResponse?> LoginAsync(LoginRequest model)
    {
        User? user = await _userService.GetByUsernameAsync(model.Username);

        // return null if user not found
        if (user == null)
            throw new LoginException();
        
        if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            throw new LoginException();

        // authentication successful so generate jwt token
        string token = GenerateJwtToken(user);
        return new LoginResponse(user, token);
    }
    
    private string GenerateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.ASCII.GetBytes(_appConfiguration.Jwt.Secret);
        
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.Username),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}