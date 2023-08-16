namespace EcoEkb.Backend.Application.Common.DTO.User.Responses;

public sealed record UserLoginResponse(string Token, string RefreshToken, DateTime ExpiresAt)
{
}