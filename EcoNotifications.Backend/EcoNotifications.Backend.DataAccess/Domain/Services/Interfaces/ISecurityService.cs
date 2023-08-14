using System.Security.Claims;

namespace EcoNotifications.Backend.DataAccess.Services.Interfaces;

public interface ISecurityService
{
    public ClaimsPrincipal? GetCurrentUser();
}