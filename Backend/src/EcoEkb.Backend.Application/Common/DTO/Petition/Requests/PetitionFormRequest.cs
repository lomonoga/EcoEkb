using System.ComponentModel.DataAnnotations;
using EcoEkb.Backend.DataAccess.Domain.Enums;
using EcoEkb.Backend.DataAccess.Domain.Models;

namespace EcoEkb.Backend.Application.Common.DTO.Requests;

public class PetitionFormRequest
{
    [Required] public string Description { get; set; }
    [Required] public Topic Topic { get; set; }
    [Required] public string Address { get; set; }
    public string CompanyName { get; set; }
    public FileData[] Attachments { get; set; }
}