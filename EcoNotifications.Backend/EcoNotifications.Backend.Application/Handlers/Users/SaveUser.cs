using EcoNotifications.Backend.Application.Common.DTO;
using EcoNotifications.Backend.DataAccess;
using EcoNotifications.Backend.DataAccess.Enums;
using EcoNotifications.Backend.DataAccess.Models;
using EcoNotifications.Backend.DataAccess.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoNotifications.Backend.Application.Handlers.Users;

public record SaveUser(UserSaveRequest UserSaveRequest) : IRequest<UserResponse>;

public class SaveUserHandler : IRequestHandler<SaveUser, UserResponse>
{
    private readonly EcoNotificationsDbContext _context;
    private readonly IHashService _hashService;
    
    public SaveUserHandler(EcoNotificationsDbContext context, IHashService hashService, ISecurityService securityService)
    {
        _context = context;
        _hashService = hashService;
    }
    
    public async Task<UserResponse> Handle(SaveUser request, CancellationToken cancellationToken)
    {
        // Mapping User
        var entityUser = request.UserSaveRequest.Adapt<User>();
        
        // Hashing password 
        entityUser.Password = _hashService.EncryptPassword(entityUser.Password);
        
        // Check for the existence of a user
        var existedUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => 
            u.Email == entityUser.Email || u.Phone == entityUser.Phone, cancellationToken);
        if (existedUser is not null)
            throw new Exception("Такой пользователь уже существет!");
        
        entityUser.Roles = new HashSet<string> { Role.Client.ToString() };
        // Adding user
        var savedUser = (await _context.Users.AddAsync(entityUser, cancellationToken)).Entity;
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(entityUser).State = EntityState.Detached;

        return savedUser.Adapt<UserResponse>();
    }
}