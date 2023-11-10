using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using EcoEkb.Backend.DataAccess.Domain.Models;
using EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EcoEkb.Backend.DataAccess.Domain.Services;

public class JwtTokenManager : ITokenManager
{
    private readonly IConfiguration _configuration;
    
    public JwtTokenManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Sid, user.Id!.Value.ToString()),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Expired, DateTime.UtcNow.ToUniversalTime()
                .Add(TimeSpan.FromHours(2)).ToString("s"))
        };
        
        var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]!));
        
        var token = new JwtSecurityToken(
            "EcoNotifications",
            null,
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            expires: DateTime.UtcNow.ToUniversalTime().Add(TimeSpan.FromHours(2)));
        
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(_configuration["JWT:SecretKey"]!)),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new UserFriendlyException("Invalid token");
            return principal;
        }
        catch (System.Exception)
        {
            throw new UserFriendlyException("Некорректный токен");
        }

    }
}