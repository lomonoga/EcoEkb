using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EcoEkb.Backend.Application.Common.DTO.Event;
using EcoEkb.Backend.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Events;

public sealed record GetEventsByUserId(Guid UserId) : IRequest<IReadOnlySet<PublicEvent>>;

public class GetEventsByUserIdHandler : IRequestHandler<GetEventsByUserId, IReadOnlySet<PublicEvent>>
{
    private readonly EcoEkbDbContext _dbContext;

    public GetEventsByUserIdHandler(
        EcoEkbDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlySet<PublicEvent>> Handle(
        GetEventsByUserId request,
        CancellationToken cancellationToken)
    {
        var result = new HashSet<PublicEvent>();
        
        var asyncResponse = _dbContext.Events
            .Where(s => 
                 s.CreatedBy == request.UserId.ToString() ||
                    (s.ExpectedParticipantsIds.Contains(request.UserId)))
            .AsAsyncEnumerable()
            .WithCancellation(cancellationToken);

        await foreach (var eventResult in asyncResponse)
        {
            result.Add(eventResult.Adapt<PublicEvent>());
        }

        return result;
    }
}