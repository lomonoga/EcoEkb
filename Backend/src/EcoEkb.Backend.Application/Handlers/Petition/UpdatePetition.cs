using EcoEkb.Backend.Application.Common.DTO.Requests;
using EcoEkb.Backend.Application.Common.DTO.Responses;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Petition;

public record UpdatePetition(PetitionStatusWithIdRequest Petition) : IRequest<PetitionResponse>;

public class UpdatePetitionHandler : IRequestHandler<UpdatePetition, PetitionResponse>
{
    private readonly EcoNotificationsDbContext _context;
    
    public UpdatePetitionHandler(EcoNotificationsDbContext context, IHashService hashService)
    {
        _context = context;
    }
    
    public async Task<PetitionResponse> Handle(UpdatePetition request, CancellationToken cancellationToken)
    {
        var petition = await _context.Petitions
            .FirstOrDefaultAsync(pet => pet.Id == request.Petition.Id, cancellationToken);
        
        if (petition is null) 
            throw new Exception("Обращения с таким ID не существует :(");
        
        petition.Status = request.Petition.Status;
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(petition).State = EntityState.Detached;
        
        return petition.Adapt<PetitionResponse>();
    }
}