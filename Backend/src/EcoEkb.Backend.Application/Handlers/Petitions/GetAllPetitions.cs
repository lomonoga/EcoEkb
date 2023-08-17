using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EcoEkb.Backend.Application.Common.DTO.Responses;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Petitions;

public record GetAllPetitions() : IRequest<List<PetitionResponse>>;

public class GetAllPetitionHandler : IRequestHandler<GetAllPetitions, List<PetitionResponse>>
{
    private readonly EcoEkbDbContext _context;
    
    public GetAllPetitionHandler(EcoEkbDbContext context, IHashService hashService)
    {
        _context = context;
    }
    
    public async Task<List<PetitionResponse>> Handle(GetAllPetitions request, CancellationToken cancellationToken)
    {
        var allPetition = await _context.Petitions
            .AsNoTracking()
            .Where(pet => !pet.IsDeleted)
            .ToListAsync(cancellationToken);
        
        return allPetition.Adapt<List<PetitionResponse>>();
    }
}