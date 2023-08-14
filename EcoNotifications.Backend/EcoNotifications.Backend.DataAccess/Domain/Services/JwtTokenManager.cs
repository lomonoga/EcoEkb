using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcoNotifications.Backend.DataAccess.Models;
using EcoNotifications.Backend.DataAccess.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EcoNotifications.Backend.DataAccess.Domain.Services;

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
            new(ClaimTypes.MobilePhone, user.Phone),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Expired, DateTime.UtcNow.ToUniversalTime().Add(TimeSpan.FromHours(2)).ToString("s"))
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
        return "";
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        return new ClaimsPrincipal();
    }
}