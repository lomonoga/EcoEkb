using System;
using System.Collections.Generic;
using EcoEkb.Backend.DataAccess.Domain.Enums;
using EcoEkb.Backend.DataAccess.Domain.Interfaces;

namespace EcoEkb.Backend.DataAccess.Domain.Models;

public class Event : IEntity
{
    public EventType EventType { get; set; }
    
    public EventStatus EventStatus { get; set; }

    public string EventName { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public string PlaceCoordinates { get; set; }
    
    public string Description { get; set; }
    
    public List<Guid> ExpectedParticipantsIds { get; set; }
    
    public List<long> ActualParticipantsCount { get; set; }
    
    public long PointsForParticipance { get; set; }
    
    public List<User> ResponsiblePersons { get; set; }
    
    public long CreatorId { get; set; }
    public Guid? Id { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}