using System.ComponentModel.DataAnnotations;
using EcoEkb.Backend.DataAccess.Enums;
using EcoEkb.Backend.DataAccess.Domain.Models;

namespace EcoEkb.Backend.Application.Common.DTO.Requests;

public class PetitionStatusWithIdRequest
{ 
    [Required] public Guid Id { get; set; }
    [Required] public StatusPetition Status { get; set; }
}