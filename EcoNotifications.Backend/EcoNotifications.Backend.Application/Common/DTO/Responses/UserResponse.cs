namespace EcoNotifications.Backend.Application.Common.DTO;

public record UserResponse(Guid? Id, string FullName, string Phone, string Email, int Coins)
{
}