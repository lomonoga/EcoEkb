using EcoNotifications.Backend.Application.Common.DTO.Responses;
using EcoNotifications.Backend.DataAccess;
using EcoNotifications.Backend.DataAccess.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoNotifications.Backend.Application.Handlers.Petition;


public record GetWithIdPetition(Guid Id) : IRequest<PetitionResponse>;

public class GetWithIdPetitionHandler : IRequestHandler<GetWithIdPetition, PetitionResponse>
{
    private readonly EcoNotificationsDbContext _context;
    
    public GetWithIdPetitionHandler(EcoNotificationsDbContext context, IHashService hashService)
    {
        _context = context;
    }
    
    public async Task<PetitionResponse> Handle(GetWithIdPetition request, CancellationToken cancellationToken)
    {
        var petition = await _context.Petitions.AsNoTracking()
            .FirstOrDefaultAsync(pet => pet.Id == request.Id, cancellationToken);
        if (petition is null) 
            throw new Exception("Обращения с таким ID не существует :(");
        return petition.Adapt<PetitionResponse>();
    }
}