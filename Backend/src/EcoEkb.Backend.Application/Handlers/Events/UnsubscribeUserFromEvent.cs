using System;
using System.Threading;
using System.Threading.Tasks;
using EcoEkb.Backend.Application.Common.DTO.Event;
using EcoEkb.Backend.Application.Common.DTO.User;
using EcoEkb.Backend.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoNotifications.Backend.Application.Handlers.Events;

public sealed record UnsubscribeUserFromEvent(
    Guid EventId,
    UserId UserId) : IRequest<PublicEvent>;

public class UnsubscribeUserFromEventHandler : IRequestHandler<UnsubscribeUserFromEvent, PublicEvent>
{
    private readonly EcoEkbDbContext _dbContext;

    public UnsubscribeUserFromEventHandler(EcoEkbDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PublicEvent> Handle(
        UnsubscribeUserFromEvent request,
        CancellationToken cancellationToken)
    {

        var publicEvent = await _dbContext
            .Events
            .FirstOrDefaultAsync(
                s => s.Id == (Guid)request.EventId,
                cancellationToken: cancellationToken);
        if (publicEvent == null)
            throw new Exception($"Не удалось найти событие {request.EventId}");
        
        if(publicEvent.ExpectedParticipantsIds == null ||
           publicEvent.ExpectedParticipantsIds.Count == 0)
            throw new Exception($"В событии {publicEvent.EventName} с Id {publicEvent.Id} и так нет участников");

        if (publicEvent.ExpectedParticipantsIds.Contains(request.UserId.Id) == false)
            throw new Exception($"В событии {publicEvent.EventName} с Id {publicEvent.Id}" +
                                $" пользователь {request.UserId} и так не участвует :)");

        publicEvent.ExpectedParticipantsIds.Remove(request.UserId.Id);

        await _dbContext.Events
            .ExecuteUpdateAsync(s =>
                s.SetProperty(d =>
                    d.ExpectedParticipantsIds, publicEvent.ExpectedParticipantsIds),
                cancellationToken: cancellationToken);
        
        return publicEvent.Adapt<PublicEvent>();
    }
}