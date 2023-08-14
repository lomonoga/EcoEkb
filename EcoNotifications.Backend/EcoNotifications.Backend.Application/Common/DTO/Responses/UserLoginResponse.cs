namespace EcoNotifications.Backend.Application.Common.DTO.Responses;

public record UserLoginResponse(string Token, string RefreshToken)
{
}