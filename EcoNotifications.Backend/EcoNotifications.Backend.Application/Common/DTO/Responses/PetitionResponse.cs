using EcoNotifications.Backend.DataAccess.Enums;

namespace EcoNotifications.Backend.Application.Common.DTO.Responses;

public class PetitionResponse
{
    public string Description { get; set; }
    public Topic Topic { get; set; }
    public string Address { get; set; }
    public string CompanyName { get; set; }
    public StatusPetition Status { get; set; }
}