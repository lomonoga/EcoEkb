namespace EcoEkb.Backend.Application.Common.DTO.Admin.Responses;

public sealed record AdminResponse(Guid? Id, string FullName, string Phone, string Email, string LastLogin)
{
}