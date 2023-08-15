using EcoEkb.Backend.DataAccess.Enums;

namespace EcoEkb.Backend.Application.Common.DTO.Responses;

public sealed class PetitionResponse
{
    public string Description { get; set; }
    public Topic Topic { get; set; }
    public string Address { get; set; }
    public string CompanyName { get; set; }
    public StatusPetition Status { get; set; }
}