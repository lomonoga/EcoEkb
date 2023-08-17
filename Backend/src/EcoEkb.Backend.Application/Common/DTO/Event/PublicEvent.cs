using System;
using System.Collections.Generic;

namespace EcoEkb.Backend.Application.Common.DTO.Event;

public class PublicEvent
{
    public long EventId { get; set; }
    
    public EventType EventType { get; set; }
    
    public EventStatus EventStatus { get; set; }
    
    public string EventName { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public string PlaceCoordinates { get; set; }
    
    public string Description { get; set; }
    
    public List<long> ExpectedParticipants { get; set; }
    
    public List<long> ActualParticipantsCount { get; set; }
    
    public long PointsForParticipance { get; set; }
    
    public List<EventResponsiblePerson> ResponsiblePersons { get; set; }
    
    public long CreatorId { get; set; }
}