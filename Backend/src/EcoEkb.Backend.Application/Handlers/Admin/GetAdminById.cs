using EcoEkb.Backend.Application.Common.DTO.Admin.Responses;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Admin;

public record GetAdminById(Guid Id) : IRequest<AdminResponse>;

public class GetAdminByIdHandler : IRequestHandler<GetAdminById, AdminResponse>
{
    private readonly EcoEkbDbContext _context;
    
    public GetAdminByIdHandler(EcoEkbDbContext context)
    {
        _context = context;
    }
    
    public async Task<AdminResponse> Handle(GetAdminById request, CancellationToken cancellationToken)
    {
        var admin = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u =>
            u.Id == request.Id && !u.IsDeleted, cancellationToken);
        
        if (admin is null )
            throw new UserFriendlyException("Такого пользователя не существует, либо он удалён");
        
        if (!admin.Roles.Contains("Admin")) throw new UserFriendlyException("Пользователь не админ!");
        
        return admin.Adapt<AdminResponse>();
    }
}