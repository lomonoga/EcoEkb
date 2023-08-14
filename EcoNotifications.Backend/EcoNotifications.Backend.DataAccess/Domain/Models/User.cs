using System.ComponentModel.DataAnnotations.Schema;
using EcoNotifications.Backend.DataAccess.Domain.Interfaces;
using EcoNotifications.Backend.DataAccess.Enums;

namespace EcoNotifications.Backend.DataAccess.Models;

public class User : IEntity
{
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Coins { get; set; }
    public DateTime LastLogin { get; set; }
    [Column(TypeName = "jsonb")] public HashSet<string> Roles { get; set; }

    #region Entity
    
    public Guid? Id { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool IsDeleted { get; set; }

    #endregion
}