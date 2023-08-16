using EcoEkb.Backend.DataAccess.Domain.Enums;

namespace EcoEkb.Backend.Application.Common.DTO.User.Responses;

public sealed record UserResponse(Guid? Id, string FullName, string Phone, string Email, int Coins, Role[] Roles)
{
}