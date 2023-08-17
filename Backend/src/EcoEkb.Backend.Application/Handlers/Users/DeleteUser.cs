using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Users;

public record DeleteUser() : IRequest<Unit>;

public class DeleteUserByIdHandler : IRequestHandler<DeleteUser, Unit>
{
    private readonly EcoEkbDbContext _context;
    private readonly ISecurityService _securityService;
    
    public DeleteUserByIdHandler(EcoEkbDbContext context, ISecurityService securityService)
    {
        _context = context;
        _securityService = securityService;
    }
    
    public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = _securityService.GetCurrentUser();
        if (user is null) 
            throw new UserFriendlyException("Вы не зарегистрированы!");
        
        var deletingUser = await _context.Users.FirstOrDefaultAsync(u => 
            u.Email == user.FindFirstValue(ClaimTypes.Email), cancellationToken);
        
        if (deletingUser is null) 
            throw new UserFriendlyException("Ошибка удаления пользователя");

        if (deletingUser.IsDeleted) throw new UserFriendlyException("Данный пользователь уже удалён");
        
        _context.Users.Remove(deletingUser);
        
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(deletingUser).State = EntityState.Detached;

        return Unit.Value;
    }
}