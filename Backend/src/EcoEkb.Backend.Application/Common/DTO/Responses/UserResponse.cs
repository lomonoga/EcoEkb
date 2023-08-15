namespace EcoEkb.Backend.Application.Common.DTO;

public sealed record UserResponse(Guid? Id, string FullName, string Phone, string Email, int Coins)
{
}