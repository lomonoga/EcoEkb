using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Admin;

public record DeleteAdmin(Guid Id) : IRequest<Unit>;

public class DeleteAdminHandler : IRequestHandler<DeleteAdmin, Unit>
{
    private readonly EcoEkbDbContext _context;
    
    public DeleteAdminHandler(EcoEkbDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteAdmin request, CancellationToken cancellationToken)
    {
        var admin = await _context.Users.FirstOrDefaultAsync(u => 
            u.Id == request.Id, cancellationToken);
        
        if (admin is null) throw new UserFriendlyException("Ошибка удаления пользователя");
        if (!admin.Roles.Contains("Admin")) throw new UserFriendlyException("Пользователь не админ!");
        if (admin.IsDeleted) throw new UserFriendlyException("Данный пользователь уже удалён!");
        
        _context.Users.Remove(admin);
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(admin).State = EntityState.Detached;

        return Unit.Value;
    }
}