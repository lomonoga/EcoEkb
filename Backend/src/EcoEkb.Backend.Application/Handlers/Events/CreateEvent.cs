using System;
using System.Threading;
using System.Threading.Tasks;
using EcoEkb.Backend.Application.Common.DTO.Event;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Models;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EcoEkb.Backend.Application.Handlers.Events;

public sealed record CreateEvent(
    PublicEvent Event) : IRequest<Guid>;

public class CreateEventHandler : IRequestHandler<CreateEvent, Guid>
{
    private readonly EcoEkbDbContext _dbContext;

    public CreateEventHandler(EcoEkbDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(
        CreateEvent request,
        CancellationToken cancellationToken)
    {
        var entityToCreate = request.Event.Adapt<Event>();

        var result = await _dbContext.Events
            .AddAsync(entityToCreate, cancellationToken);

        if (result.Entity.Id == null)
            throw new Exception("Событие не было найдено");
        return result.Entity.Id.Value;
    }
}