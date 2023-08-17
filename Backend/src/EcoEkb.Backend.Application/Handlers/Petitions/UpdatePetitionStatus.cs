using EcoEkb.Backend.Application.Common.DTO.Petition.Requests;
using EcoEkb.Backend.Application.Common.DTO.Responses;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Petition;

public record UpdatePetitionStatus(PetitionStatusWithIdRequest Petition) : IRequest<PetitionResponse>;

public class UpdatePetitionHandler : IRequestHandler<UpdatePetitionStatus, PetitionResponse>
{
    private readonly EcoEkbDbContext _context;
    
    public UpdatePetitionHandler(EcoEkbDbContext context, IHashService hashService)
    {
        _context = context;
    }
    
    public async Task<PetitionResponse> Handle(UpdatePetitionStatus request, CancellationToken cancellationToken)
    {
        var petition = await _context.Petitions
            .FirstOrDefaultAsync(pet => pet.Id == request.Petition.Id 
                                        && !pet.IsDeleted, cancellationToken);
        
        if (petition is null) 
            throw new UserFriendlyException("Обращения с таким ID не существует :(");
        
        petition.Status = request.Petition.Status;
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(petition).State = EntityState.Detached;
        
        return petition.Adapt<PetitionResponse>();
    }
}