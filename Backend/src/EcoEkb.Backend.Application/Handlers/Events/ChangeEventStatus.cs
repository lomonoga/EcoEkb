using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EcoEkb.Backend.Application.Common.DTO.Event;
using EcoEkb.Backend.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Events;

public sealed record ChangeEventStatus(
    EventStatus NewStatus,
    Guid EventId) : IRequest<PublicEvent?>;

public class ChangeEventStatusHandler : IRequestHandler<ChangeEventStatus, PublicEvent?>
{
    private EcoEkbDbContext _dbContext;

    public ChangeEventStatusHandler(EcoEkbDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //TODO Добавить миграцию на статус
    public async Task<PublicEvent?> Handle(
        ChangeEventStatus request,
        CancellationToken cancellationToken)
    {
        await _dbContext.Events
            .Where(s => s.Id == request.EventId)
            .ExecuteUpdateAsync(
                s => s
                    .SetProperty(d =>
                        d.EventStatus, (DataAccess.Domain.Enums.EventStatus)request.NewStatus),
                cancellationToken);

        var result = await _dbContext.Events.FirstOrDefaultAsync(
            s => s.Id == request.EventId,
            cancellationToken);

        return result.Adapt<PublicEvent>();
    }
}