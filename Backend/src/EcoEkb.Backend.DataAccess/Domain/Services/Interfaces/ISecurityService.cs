using System.Security.Claims;

namespace EcoEkb.Backend.DataAccess.Services.Interfaces;

public interface ISecurityService
{
    public ClaimsPrincipal? GetCurrentUser();
}