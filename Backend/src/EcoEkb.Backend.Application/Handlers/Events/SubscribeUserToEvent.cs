using System;
using System.Threading;
using System.Threading.Tasks;
using EcoEkb.Backend.Application.Common.DTO.Event;
using EcoEkb.Backend.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Events;

public sealed record SubscribeUserToEvent(
    Guid EventId,
    Guid UserId) : IRequest<PublicEvent>;

public class SubscribeUserToEventHandler : IRequestHandler<SubscribeUserToEvent, PublicEvent>
{
    private readonly EcoEkbDbContext _dbContext;

    public SubscribeUserToEventHandler(EcoEkbDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PublicEvent> Handle(
        SubscribeUserToEvent request,
        CancellationToken cancellationToken)
    {

        var publicEvent = await _dbContext
            .Events
            .FirstOrDefaultAsync(
                s => s.Id == request.EventId,
                cancellationToken: cancellationToken);
        if (publicEvent == null)
            throw new Exception($"Не удалось найти событие {request.EventId}");

        if (publicEvent.ExpectedParticipantsIds!.Contains(request.UserId) == true)
            throw new Exception($"В событии {publicEvent.EventName} с Id {publicEvent.Id}" +
                                $" пользователь {request.UserId} и так участвует :)");

        publicEvent.ExpectedParticipantsIds.Add(request.UserId);

        await _dbContext.Events
            .ExecuteUpdateAsync(s =>
                    s.SetProperty(d =>
                        d.ExpectedParticipantsIds, publicEvent.ExpectedParticipantsIds),
                cancellationToken: cancellationToken);

        return publicEvent.Adapt<PublicEvent>();
    }
}
