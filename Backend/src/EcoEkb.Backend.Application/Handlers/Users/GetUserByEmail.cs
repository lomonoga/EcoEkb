using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Users;

public record GetUserByEmail(string Email) : IRequest<User?>;

public class GetUserByEmailHandler : IRequestHandler<GetUserByEmail, User?>
{
    private readonly EcoEkbDbContext _context;

    public GetUserByEmailHandler(EcoEkbDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Получение НЕ УДАЛЁННОГО юзера из бд по Email
    /// !!! Если не существует то выдает null !!!
    /// </summary>
    /// <param name="request">Email</param>
    /// <param name="cancellationToken"></param>
    /// <returns>User or null</returns>
    public async Task<User?> Handle(GetUserByEmail request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => 
            ! u.IsDeleted 
            && request.Email == u.Email, cancellationToken);
        return user;
    }
}