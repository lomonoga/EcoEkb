using EcoEkb.Backend.DataAccess.Domain.Enums;
using EcoEkb.Backend.DataAccess.Domain.Interfaces;

namespace EcoEkb.Backend.DataAccess.Domain.Models;

public class Company : IEntity
{
    public string NameCompany { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public District[] ControlledAreas { get; set; }
    public Topic[] TypesOfResponsibility { get; set; }
    
    #region Entity
    
    public Guid? Id { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    
    #endregion
}