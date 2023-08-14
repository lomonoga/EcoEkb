using System.ComponentModel.DataAnnotations;
using EcoNotifications.Backend.DataAccess.Domain.Models;
using EcoNotifications.Backend.DataAccess.Enums;

namespace EcoNotifications.Backend.Application.Common.DTO.Requests;

public class PetitionStatusWithIdRequest
{ 
    [Required] public Guid Id { get; set; }
    [Required] public StatusPetition Status { get; set; }
}