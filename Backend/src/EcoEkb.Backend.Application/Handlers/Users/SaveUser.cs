using EcoEkb.Backend.Application.Common.DTO;
using EcoEkb.Backend.Application.Common.DTO.Requests;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using EcoEkb.Backend.DataAccess.Domain.Models;
using EcoEkb.Backend.DataAccess.Enums;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using EcoEkb.Backend.DataAccess.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Users;

public record SaveUser(UserSaveRequest UserSaveRequest) : IRequest<UserResponse>;

public class SaveUserHandler : IRequestHandler<SaveUser, UserResponse>
{
    private readonly EcoEkbDbContext _context;
    private readonly IHashService _hashService;
    
    public SaveUserHandler(EcoEkbDbContext context, IHashService hashService)
    {
        _context = context;
        _hashService = hashService;
    }
    
    public async Task<UserResponse> Handle(SaveUser request, CancellationToken cancellationToken)
    {
        // Mapping User
        var entityUser = request.UserSaveRequest.Adapt<User>();
        
        // Check for the existence of a user
        var existedUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => 
            ! u.IsDeleted
            && u.Email == entityUser.Email, cancellationToken);
        if (existedUser is not null)
            throw new UserFriendlyException("Такой пользователь уже существет!");
        
        // Hashing password and adding role of Client
        entityUser.Password = _hashService.EncryptPassword(entityUser.Password);
        entityUser.Roles = new HashSet<string> { Role.Client.ToString() };
        
        // Adding user
        var savedUser = (await _context.Users.AddAsync(entityUser, cancellationToken)).Entity;
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(entityUser).State = EntityState.Detached;

        return savedUser.Adapt<UserResponse>();
    }
}