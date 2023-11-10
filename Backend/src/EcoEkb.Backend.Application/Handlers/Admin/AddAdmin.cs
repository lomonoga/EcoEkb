using EcoEkb.Backend.Application.Common.DTO.Admin.Requests;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Enums;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using EcoEkb.Backend.DataAccess.Domain.Models;
using EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Admin;

public record AddAdmin(AdminAddRequest UserAddRequest) : IRequest<Unit>;

public class AddAdminHandler : IRequestHandler<AddAdmin, Unit>
{
    private readonly EcoEkbDbContext _context;
    private readonly IHashService _hashService;
    private readonly ISecurityService _securityService;
    
    public AddAdminHandler(EcoEkbDbContext context, IHashService hashService, ISecurityService securityService)
    {
        _context = context;
        _hashService = hashService;
        _securityService = securityService;
    }
    
    public async Task<Unit> Handle(AddAdmin request, CancellationToken cancellationToken)
    {
        // Mapping UserAdmin
        var entityUser = request.UserAddRequest.Adapt<User>();
        
        // Check for the existence of a user
        var existedUser = await _context.Users.FirstOrDefaultAsync(u =>
            u.Email == entityUser.Email, cancellationToken);
        if (existedUser is not null && !existedUser.IsDeleted)
            throw new UserFriendlyException("Пользователь с таким email уже существет!");
        
        // Hashing password and adding role of Client
        entityUser.Password = _hashService.EncryptPassword(entityUser.Password);
        entityUser.Roles = new HashSet<string> { /*Role.Client.ToString(),*/ Role.Admin.ToString() };
        
        //Add or change user
        if (existedUser is not null)
        {
            existedUser.FullName = entityUser.FullName;
            existedUser.Password = entityUser.Password;
            existedUser.Roles = entityUser.Roles;
            existedUser.Phone = entityUser.Phone;
            existedUser.RefreshToken = null;
            existedUser.Coins = 0;
            existedUser.CreatedOn = DateTime.UtcNow.ToUniversalTime();
            existedUser.CreatedBy = _securityService.GetCurrentUser()?.Identity?.Name ?? "System";
            existedUser.IsDeleted = false;
        }
        else
            await _context.Users.AddAsync(entityUser, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(entityUser).State = EntityState.Detached;

        return Unit.Value;
    }
}