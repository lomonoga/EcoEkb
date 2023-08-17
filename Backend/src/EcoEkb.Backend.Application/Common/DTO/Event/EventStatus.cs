namespace EcoEkb.Backend.Application.Common.DTO.Event;

public enum EventStatus
{
    Unknown = 0,
    InProgress,
    Canceled,
    WillBeLaunchedSoon,
    Finished
}