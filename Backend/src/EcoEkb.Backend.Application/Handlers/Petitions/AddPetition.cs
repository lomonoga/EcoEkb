using EcoEkb.Backend.Application.Common.DTO.Requests;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Enums;
using EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Petitions;

public record AddPetition(PetitionFormRequest PetitionFormRequest) : IRequest<Unit>;

public class AddPetitionHandler : IRequestHandler<AddPetition, Unit>
{
    private readonly EcoEkbDbContext _context;
    private readonly ISendEmail _sendEmail;
    
    public AddPetitionHandler(EcoEkbDbContext context, ISendEmail sendEmail)
    {
        _context = context;

        _sendEmail = sendEmail;
    }
    
    public async Task<Unit> Handle(AddPetition request, CancellationToken cancellationToken)
    {
        var entityPetition = request.PetitionFormRequest.Adapt<DataAccess.Models.Petition>();
        entityPetition.Status = StatusPetition.New;
        
        var savedPetition = await _context.Petitions.AddAsync(entityPetition, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(entityPetition).State = EntityState.Detached;

        await _sendEmail.SendEmailAsync("bizi1298@gmail.com", 
            savedPetition.Entity.Topic.ToString(), savedPetition.Entity.Address);
            
        return Unit.Value;
    }
}