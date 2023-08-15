using EcoEkb.Backend.Application.Common.DTO.Requests;
using EcoEkb.Backend.Application.Common.DTO.Responses;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;
using EcoEkb.Backend.DataAccess.Enums;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Petition;

public record AddPetition(PetitionFormRequest PetitionFormRequest) : IRequest<PetitionResponse>;

public class AddPetitionHandler : IRequestHandler<AddPetition, PetitionResponse>
{
    private readonly EcoEkbDbContext _context;
    private readonly IHashService _hashService;
    private readonly ISendEmail _sendEmail;
    
    public AddPetitionHandler(EcoEkbDbContext context, IHashService hashService, ISendEmail sendEmail)
    {
        _context = context;
        _hashService = hashService;
        _sendEmail = sendEmail;
    }
    
    public async Task<PetitionResponse> Handle(AddPetition request, CancellationToken cancellationToken)
    {
        var entityPetition = request.PetitionFormRequest.Adapt<DataAccess.Models.Petition>();
        entityPetition.Status = StatusPetition.New;
        
        var savedPetition = await _context.Petitions.AddAsync(entityPetition, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(entityPetition).State = EntityState.Detached;

        await _sendEmail.SendEmailAsync("bizi1298@gmail.com", 
            savedPetition.Entity.Topic.ToString(), savedPetition.Entity.Address);
            
        return savedPetition.Adapt<PetitionResponse>();
    }
}