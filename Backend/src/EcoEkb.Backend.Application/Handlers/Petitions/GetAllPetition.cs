using EcoEkb.Backend.Application.Common.DTO.Responses;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Petitions;

public record GetAllPetition() : IRequest<List<PetitionResponse>>;

public class GetAllPetitionHandler : IRequestHandler<GetAllPetition, List<PetitionResponse>>
{
    private readonly EcoEkbDbContext _context;
    
    public GetAllPetitionHandler(EcoEkbDbContext context, IHashService hashService)
    {
        _context = context;
    }
    
    public async Task<List<PetitionResponse>> Handle(GetAllPetition request, CancellationToken cancellationToken)
    {
        var allPetition = await _context.Petitions.AsNoTracking().ToListAsync(cancellationToken);

        return allPetition.Adapt<List<PetitionResponse>>();
    }
}