using EcoNotifications.Backend.DataAccess.Domain.Interfaces;
using EcoNotifications.Backend.DataAccess.Models;
using EcoNotifications.Backend.DataAccess.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoNotifications.Backend.DataAccess;

public class EcoNotificationsDbContext : DbContext
{
    private readonly ISecurityService _securityService;
    public EcoNotificationsDbContext(DbContextOptions<EcoNotificationsDbContext> options, 
        ISecurityService securityService) : base(options)
    {
        _securityService = securityService;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Petition> Petitions { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        foreach (var property in entityType.GetProperties())
        {
            var propertyType = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;
            if (propertyType.IsEnum)
                property.SetProviderClrType(typeof(string));
        }

        builder.Entity<User>().HasAlternateKey(x => x.Email);
        builder.Entity<User>().HasAlternateKey(x => x.Phone);
        
        base.OnModelCreating(builder);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SoftDelete();
        WriteAudit();
        WriteTime();
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    private void SoftDelete()
    {
        foreach (var entry in ChangeTracker.Entries<IHistoricalEntity>())
            switch (entry.State)
            {
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                    break;
                case EntityState.Modified:
                    break;
                case EntityState.Added:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
    }

    private void WriteAudit()
    {
        var user = _securityService.GetCurrentUser();
        foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
            switch (entry.State)
            {
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = user?.Identity?.Name ?? "Admin";
                    break;
                case EntityState.Added:
                    entry.Entity.ModifiedBy = user?.Identity?.Name ?? "Admin";
                    entry.Entity.CreatedBy = user?.Identity?.Name ?? "Admin";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
    }

    private void WriteTime()
    {
        foreach (var entry in ChangeTracker.Entries<ITimedEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedOn = DateTime.UtcNow;
                    break;
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.UtcNow.ToUniversalTime();
                    entry.Entity.ModifiedOn = DateTime.UtcNow.ToUniversalTime();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}