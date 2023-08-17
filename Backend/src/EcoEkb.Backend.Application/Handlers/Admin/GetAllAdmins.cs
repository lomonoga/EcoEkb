using EcoEkb.Backend.Application.Common.DTO.Admin.Responses;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Admin;

public record GetAllAdmins() : IRequest<List<AdminResponse>>;

public class GetAllAdminsHandler : IRequestHandler<GetAllAdmins, List<AdminResponse>>
{
    private readonly EcoEkbDbContext _context;
    
    public GetAllAdminsHandler(EcoEkbDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<AdminResponse>> Handle(GetAllAdmins request, CancellationToken cancellationToken)
    {
        var hashSetAdmin = new HashSet<string> { Role.Admin.ToString() };
        var admin = await _context.Users
            .AsNoTracking()
            .Where(user => !user.IsDeleted && user.Roles == hashSetAdmin)
            .ToListAsync(cancellationToken);
        
        return admin.Adapt<List<AdminResponse>>();
    }
}