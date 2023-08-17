using System.Collections.Generic;
using EcoEkb.Backend.Application.Common.DTO.Event;

namespace DefaultNamespace;

public class GetEventsByUserIdResponse
{
    public List<PublicEvent> Events { get; set; }
}