using EcoNotifications.Backend.Application.Common.DTO.Responses;
using EcoNotifications.Backend.DataAccess;
using EcoNotifications.Backend.DataAccess.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoNotifications.Backend.Application.Handlers.Petition;

public record GetAllPetition() : IRequest<List<PetitionResponse>>;

public class GetAllPetitionHandler : IRequestHandler<GetAllPetition, List<PetitionResponse>>
{
    private readonly EcoNotificationsDbContext _context;
    
    public GetAllPetitionHandler(EcoNotificationsDbContext context, IHashService hashService)
    {
        _context = context;
    }
    
    public async Task<List<PetitionResponse>> Handle(GetAllPetition request, CancellationToken cancellationToken)
    {
        var allPetition = await _context.Petitions.AsNoTracking().ToListAsync(cancellationToken);

        return allPetition.Adapt<List<PetitionResponse>>();
    }
}