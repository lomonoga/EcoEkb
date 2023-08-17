namespace EcoEkb.Backend.Application.Common.DTO.Event.Requests;

public class ChangeEventStatusRequest
{
    public long EventId { get; set; }
    
    public EventStatus NewStatus { get; set; }
}