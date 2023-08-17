using System;
using System.ComponentModel.DataAnnotations;
using EcoEkb.Backend.DataAccess.Domain.Enums;

namespace EcoEkb.Backend.Application.Common.DTO.Petition.Requests;

public class PetitionStatusWithIdRequest
{ 
    [Required] public Guid Id { get; set; }
    [Required] public StatusPetition Status { get; set; }
}