using EcoEkb.Backend.Application.Common.DTO.User.Requests;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using EcoEkb.Backend.DataAccess.Domain.Models;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Auth;

public record LoginUser(UserLoginRequest LoginRequest) : IRequest<User>;

public class LoginUserHandler : IRequestHandler<LoginUser, User>
{
    private readonly EcoEkbDbContext _context;
    private readonly IHashService _hashService;
    
    public LoginUserHandler(EcoEkbDbContext context, IHashService hashService)
    {
        _context = context;
        _hashService = hashService;
    }
    
    public async Task<User> Handle(LoginUser request, CancellationToken cancellationToken)
    {
        // Checking the existence of the user and the correctness of his login and password
        if (request.LoginRequest.Email is null)
            throw new Exception("Такого пользователя не существует");
        var hashedPassword = _hashService.EncryptPassword(request.LoginRequest.Password);
        var user = await _context.Users.FirstOrDefaultAsync(u => request.LoginRequest.Email == u.Email
            && (request.LoginRequest.Password == u.Password
                || hashedPassword == u.Password), cancellationToken);
        if (user is null) throw new UserFriendlyException("Такого пользователя не существует или неверные данные");
        
        // Changing time last login 
        user.LastLogin = DateTime.UtcNow.ToUniversalTime();
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(user).State = EntityState.Detached;

        return user;
    }
}