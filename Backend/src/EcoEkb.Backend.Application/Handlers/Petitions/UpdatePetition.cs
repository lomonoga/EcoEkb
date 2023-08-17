using System.Threading;
using System.Threading.Tasks;
using EcoEkb.Backend.Application.Common.DTO.Petition.Requests;
using EcoEkb.Backend.Application.Common.DTO.Requests;
using EcoEkb.Backend.Application.Common.DTO.Responses;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Petitions;

public record UpdatePetition(PetitionStatusWithIdRequest Petition) : IRequest<PetitionResponse>;

public class UpdatePetitionHandler : IRequestHandler<UpdatePetition, PetitionResponse>
{
    private readonly EcoEkbDbContext _context;
    
    public UpdatePetitionHandler(EcoEkbDbContext context, IHashService hashService)
    {
        _context = context;
    }
    
    public async Task<PetitionResponse> Handle(UpdatePetition request, CancellationToken cancellationToken)
    {
        var petition = await _context.Petitions
            .FirstOrDefaultAsync(pet => pet.Id == request.Petition.Id, cancellationToken);
        
        if (petition is null) 
            throw new UserFriendlyException("Обращения с таким ID не существует :(");
        
        petition.Status = request.Petition.Status;
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(petition).State = EntityState.Detached;
        
        return petition.Adapt<PetitionResponse>();
    }
}