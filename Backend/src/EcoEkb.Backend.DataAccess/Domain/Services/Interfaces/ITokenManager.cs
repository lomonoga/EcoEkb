using System.Security.Claims;
using EcoEkb.Backend.DataAccess.Domain.Models;

namespace EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;

public interface ITokenManager
{
    public string GenerateToken(User user);
    public string GenerateRefreshToken();
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}