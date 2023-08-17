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

public sealed record GetAllEvents() : IRequest<IReadOnlySet<PublicEvent>>;

public class GetAllEventsHandler : IRequestHandler<GetAllEvents, IReadOnlySet<PublicEvent>>
{
    private readonly EcoEkbDbContext _dbContext;

    public GetAllEventsHandler(EcoEkbDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlySet<PublicEvent>> Handle(
        GetAllEvents request,
        CancellationToken cancellationToken)
    {
        var result = await _dbContext.Events.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

        return result.Adapt<IReadOnlySet<PublicEvent>>().ToHashSet();
    }
}