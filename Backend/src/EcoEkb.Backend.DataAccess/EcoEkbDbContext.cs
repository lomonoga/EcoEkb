using EcoEkb.Backend.DataAccess.Domain.Interfaces;
using EcoEkb.Backend.DataAccess.Domain.Models;
using EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;
using EcoEkb.Backend.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.DataAccess;

public class EcoEkbDbContext : DbContext
{
    private readonly ISecurityService _securityService;
    public EcoEkbDbContext(DbContextOptions<EcoEkbDbContext> options, 
        ISecurityService securityService) : base(options)
    {
        _securityService = securityService;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Petition> Petitions { get; set; } = default!;
    
    public DbSet<Company> Companies { get; set; } = default!;
    
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
        //builder.Entity<User>().HasAlternateKey(x => x.Phone);
        
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
                    entry.Entity.ModifiedBy = user?.Identity?.Name ?? "System";
                    break;
                case EntityState.Added:
                    entry.Entity.ModifiedBy = user?.Identity?.Name ?? "System";
                    entry.Entity.CreatedBy = user?.Identity?.Name ?? "System";
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