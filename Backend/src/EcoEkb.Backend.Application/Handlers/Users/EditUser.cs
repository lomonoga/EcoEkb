using System.Security.Claims;
using EcoEkb.Backend.Application.Common.DTO;
using EcoEkb.Backend.Application.Common.DTO.Requests;
using EcoEkb.Backend.Application.Common.DTO.User.Requests;
using EcoEkb.Backend.Application.Common.DTO.User.Responses;
using EcoEkb.Backend.DataAccess;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcoEkb.Backend.Application.Handlers.Users;

public record EditUser(UserEditRequest UserSaveRequest) : IRequest<UserResponse>;

public class EditUserHandler : IRequestHandler<EditUser, UserResponse>
{
    private readonly EcoEkbDbContext _context;
    private readonly IHashService _hashService;
    private readonly ISecurityService _securityService;
    
    public EditUserHandler(EcoEkbDbContext context, IHashService hashService, ISecurityService securityService)
    {
        _context = context;
        _hashService = hashService;
        _securityService = securityService;
    }
    
    public async Task<UserResponse> Handle(EditUser request, CancellationToken cancellationToken)
    {
        var user = _securityService.GetCurrentUser();
        if (user is null) 
            throw new UserFriendlyException("Вы не зарегистрированы!");
        
        var editUser = await _context.Users.FirstOrDefaultAsync(u => 
            ! u.IsDeleted
            && u.Email == user.FindFirstValue(ClaimTypes.Email), cancellationToken);
        
        if (editUser is null) 
            throw new UserFriendlyException("Ошибка изменения пользователя");

        editUser.Phone = request.UserSaveRequest.Phone ?? editUser.Phone;
        editUser.FullName = request.UserSaveRequest.FullName ?? editUser.FullName;
        editUser.Password = request.UserSaveRequest.Password is null
            ? editUser.Password
            : _hashService.EncryptPassword(request.UserSaveRequest.Password);
        
        await _context.SaveChangesAsync(cancellationToken);
        _context.Entry(editUser).State = EntityState.Detached;

        return editUser.Adapt<UserResponse>();
    }
}