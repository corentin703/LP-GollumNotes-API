using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CoverotNimorin.GollumChat.Server.Configuration;
using CoverotNimorin.GollumChat.Server.Constants;
using CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoverotNimorin.GollumChat.Server.Middleware;

public class JwtAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppConfiguration _appConfiguration;

    public JwtAuthMiddleware(RequestDelegate next, IOptions<AppConfiguration> appSettings)
    {
        _next = next;
        _appConfiguration = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserRepository userRepository)
    {
        string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            await AttachUserToContextAsync(context, userRepository, token);

        await _next(context);
    }

    private async Task AttachUserToContextAsync(HttpContext context, IUserRepository userRepository, string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.ASCII.GetBytes(_appConfiguration.Jwt.Secret);
        
        tokenHandler.ValidateToken(
            token,
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, 
            out SecurityToken validatedToken
        );

        JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
        string userId = jwtToken.Claims.First(x => x.Type == "id").Value;

        // attach user to context on successful jwt validation
        context.Items[AuthConstants.HttpContextCurrentUser] = await userRepository.GetByIdAsync(userId);
    }
}