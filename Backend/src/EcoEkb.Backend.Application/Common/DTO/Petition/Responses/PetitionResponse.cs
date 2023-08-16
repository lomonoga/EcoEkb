using EcoEkb.Backend.DataAccess.Domain.Enums;

namespace EcoEkb.Backend.Application.Common.DTO.Responses;

public sealed record PetitionResponse(string Description, Topic Topic, string Address, 
    string CompanyName, StatusPetition Status)
{
}