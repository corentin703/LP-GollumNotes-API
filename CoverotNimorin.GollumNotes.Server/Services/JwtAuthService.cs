using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoverotNimorin.GollumNotes.Server.Configuration;
using CoverotNimorin.GollumNotes.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumNotes.Server.Contracts.Services;
using CoverotNimorin.GollumNotes.Server.Entities;
using CoverotNimorin.GollumNotes.Server.Exceptions.Auth;
using CoverotNimorin.GollumNotes.Server.Exceptions.Entities;
using CoverotNimorin.GollumNotes.Server.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoverotNimorin.GollumNotes.Server.Services;

public class JwtAuthService : IAuthService
{
    private readonly AppConfiguration _appConfiguration;
    private readonly IUserRepository _userRepository;

    public JwtAuthService(IOptions<AppConfiguration> appConfiguration, IUserRepository userRepository)
    {
        _appConfiguration = appConfiguration.Value;
        _userRepository = userRepository;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest model)
    {
        User? user;

        try
        {
            await _userRepository.GetByUsernameAsync(model.Username);
            throw new UserAlreadyExistsException();
        }
        catch (UserNotFoundByUsernameException)
        {
            //
        }

        user = new User()
        {
            Username = model.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
        };

        _userRepository.Add(user);
        await _userRepository.SaveChangesAsync();

        return new RegisterResponse(user);
    }
    
    public async Task<LoginResponse> LoginAsync(LoginRequest model)
    {
        User user;

        try
        {
            user = await _userRepository.GetByUsernameAsync(model.Username);
        }
        catch (UserNotFoundByUsernameException)
        {
            // Ne pas révéler la cause de l'échec de la connexion
            throw new LoginException();
        }

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