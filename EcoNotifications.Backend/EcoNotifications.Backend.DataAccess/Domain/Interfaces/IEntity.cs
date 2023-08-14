namespace EcoNotifications.Backend.DataAccess.Domain.Interfaces;

public interface IGuidEntity
{
    Guid? Id { get; set; }
}

public interface IAuditedEntity : ITimedEntity
{
    string? CreatedBy { get; set; }
    string? ModifiedBy { get; set; }
}

public interface ITimedEntity : IGuidEntity
{
    DateTime? CreatedOn { get; set; }
    DateTime? ModifiedOn { get; set; }
}

public interface IHistoricalEntity
{
    bool IsDeleted { get; set; }
}

public interface IEntity : IAuditedEntity, IHistoricalEntity
{
}
