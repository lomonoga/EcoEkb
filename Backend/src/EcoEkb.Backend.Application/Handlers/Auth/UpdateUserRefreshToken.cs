using EcoEkb.Backend.Application.Handlers.Users;
using EcoEkb.Backend.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Auth;

public record UpdateUserRefreshToken(Guid Id, string RefreshToken) : IRequest<Unit>;

public class UpdateUserRefreshTokenHandler : IRequestHandler<UpdateUserRefreshToken, Unit>
{
    private readonly EcoEkbDbContext _context;

    public UpdateUserRefreshTokenHandler(EcoEkbDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateUserRefreshToken request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => 
            ! u.IsDeleted 
            && request.Id == u.Id, cancellationToken);
        if (user is null) return Unit.Value;

        user.RefreshToken = request.RefreshToken;
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(user).State = EntityState.Detached;

        return Unit.Value;
    }
}
