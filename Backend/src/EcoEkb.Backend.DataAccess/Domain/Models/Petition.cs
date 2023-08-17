using System.ComponentModel.DataAnnotations.Schema;
using EcoEkb.Backend.DataAccess.Domain.Enums;
using EcoEkb.Backend.DataAccess.Domain.Interfaces;
using EcoEkb.Backend.DataAccess.Domain.Models;

namespace EcoEkb.Backend.DataAccess.Models;

public class Petition : IEntity
{
    public string Description { get; set; }
    public Topic Topic { get; set; }
    public string Address { get; set; }
    public string CompanyName { get; set; }
    public StatusPetition Status { get; set; }
    [Column(TypeName = "jsonb")] public FileData[] Attachments { get; set; }
    
    #region Entity
    
    public Guid? Id { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }

    #endregion
}