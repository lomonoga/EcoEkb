using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Petitions;

public record DeletePetitionWithId(Guid Id) : IRequest<Unit>;

public class DeletePetitionWithIdHandler : IRequestHandler<DeletePetitionWithId, Unit>
{
    private readonly EcoEkbDbContext _context;
    
    public DeletePetitionWithIdHandler(EcoEkbDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeletePetitionWithId request, CancellationToken cancellationToken)
    {
        var petition = await _context.Petitions
            .FirstOrDefaultAsync(pet => pet.Id == request.Id 
                                        && !pet.IsDeleted, cancellationToken);
        if (petition is null) 
            throw new UserFriendlyException("Обращения с таким ID не существует или она уже удалена :(");

        _context.Petitions.Remove(petition);
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(petition).State = EntityState.Detached;
        
        return Unit.Value;
    }
}