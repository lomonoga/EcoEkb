using System.Security.Claims;
using EcoEkb.Backend.DataAccess.Domain.Models;
using EcoEkb.Backend.DataAccess.Models;

namespace EcoEkb.Backend.DataAccess.Services.Interfaces;

public interface ITokenManager
{
    public string GenerateToken(User user);
    public string GenerateRefreshToken();
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}