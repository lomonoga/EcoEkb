using System;
using System.Threading;
using System.Threading.Tasks;
using EcoEkb.Backend.Application.Common.DTO.Requests;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Users;

public record GetUserById(Guid Id) : IRequest<User>;

public class GetUserByIdHandler : IRequestHandler<GetUserById, User>
{
    private readonly EcoEkbDbContext _context;

    public GetUserByIdHandler(EcoEkbDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Получение не удалённого юзера из бд по Id
    /// !!! Если не существует то выдает null !!!
    /// </summary>
    /// <param name="request">Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>User or null</returns>
    public async Task<User> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => 
            ! u.IsDeleted 
            && request.Id == u.Id, cancellationToken);
        return user;
    }
}