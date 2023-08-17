using System;
using System.Threading;
using System.Threading.Tasks;
using EcoEkb.Backend.Application.Common.DTO.Responses;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Petitions;


public record GetWithIdPetition(Guid Id) : IRequest<PetitionResponse>;

public class GetWithIdPetitionHandler : IRequestHandler<GetWithIdPetition, PetitionResponse>
{
    private readonly EcoEkbDbContext _context;
    
    public GetWithIdPetitionHandler(EcoEkbDbContext context, IHashService hashService)
    {
        _context = context;
    }
    
    public async Task<PetitionResponse> Handle(GetWithIdPetition request, CancellationToken cancellationToken)
    {
        var petition = await _context.Petitions.AsNoTracking()
            .FirstOrDefaultAsync(pet => pet.Id == request.Id, cancellationToken);
        if (petition is null) 
            throw new UserFriendlyException("Обращения с таким ID не существует :(");
        return petition.Adapt<PetitionResponse>();
    }
}