using System.Security.Claims;
using EcoNotifications.Backend.DataAccess.Models;

namespace EcoNotifications.Backend.DataAccess.Services.Interfaces;

public interface ITokenManager
{
    public string GenerateToken(User user);
    public string GenerateRefreshToken();
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}