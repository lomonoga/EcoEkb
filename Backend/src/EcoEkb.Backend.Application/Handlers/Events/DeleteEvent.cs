using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EcoEkb.Backend.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Events;

public sealed record DeleteEvent(
    Guid EventId) : IRequest<bool>;

public class DeleteEventHandler : IRequestHandler<DeleteEvent, bool>
{
    private readonly EcoEkbDbContext _dbContext;

    public DeleteEventHandler(EcoEkbDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteEvent request, CancellationToken cancellationToken)
    {
        await _dbContext.Events
            .Where(s => s.Id == request.EventId)
            .ExecuteDeleteAsync(
            cancellationToken);

        return true;
    }
}
    
    