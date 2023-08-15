namespace EcoEkb.Backend.Application.Common.DTO.Responses;

public sealed record UserLoginResponse(string Token, string RefreshToken, DateTime ExpiresAt)
{
}