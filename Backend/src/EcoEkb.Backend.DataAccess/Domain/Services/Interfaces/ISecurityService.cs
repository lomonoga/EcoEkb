using System.Security.Claims;

namespace EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;

public interface ISecurityService
{
    public ClaimsPrincipal? GetCurrentUser();
}