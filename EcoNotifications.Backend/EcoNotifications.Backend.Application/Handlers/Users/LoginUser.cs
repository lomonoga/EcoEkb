using EcoNotifications.Backend.Application.Common.DTO;
using EcoNotifications.Backend.DataAccess;
using EcoNotifications.Backend.DataAccess.Models;
using EcoNotifications.Backend.DataAccess.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoNotifications.Backend.Application.Handlers.Users;

public record LoginUser(UserLoginRequest LoginRequest) : IRequest<User>;

public class LoginUserHandler : IRequestHandler<LoginUser, User>
{
    private readonly EcoNotificationsDbContext _context;
    private readonly IHashService _hashService;
    
    public LoginUserHandler(EcoNotificationsDbContext context, IHashService hashService)
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
                                                                 && hashedPassword == u.Password, cancellationToken);
        if (user is null) throw new Exception("Такого пользователя не существует или неверные данные");
        
        // Changing time last login 
        user.LastLogin = DateTime.UtcNow.ToUniversalTime();
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(user).State = EntityState.Detached;

        return user;
    }
}