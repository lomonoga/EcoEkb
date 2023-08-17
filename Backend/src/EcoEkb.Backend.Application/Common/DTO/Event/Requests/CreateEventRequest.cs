using System;
using System.Collections.Generic;
using EcoEkb.Backend.Application.Common.DTO.Event;

namespace DefaultNamespace;

public class CreateEventRequest
{
    public long EventId { get; set; }
    
    public EventType EventType { get; set; }
    
    public string EventName { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public string PlaceCoordinates { get; set; }
    
    public string Description { get; set; }
    
    public long PointsForParticipance { get; set; }
    
    public IReadOnlySet<EventResponsiblePerson> ResponsiblePersons { get; set; }
    
    public long CreatorId { get; set; }
}