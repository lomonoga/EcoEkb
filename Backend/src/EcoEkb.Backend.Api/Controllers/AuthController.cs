using System.Security.Claims;
using EcoEkb.Backend.Application.Common.DTO.User.Requests;
using EcoEkb.Backend.Application.Common.DTO.User.Responses;
using EcoEkb.Backend.Application.Handlers.Auth;
using EcoEkb.Backend.Application.Handlers.Users;
using EcoEkb.Backend.DataAccess.Domain.Exception;
using EcoEkb.Backend.DataAccess.Domain.Models;
using EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoEkb.Backend.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ISecurityService _securityService;
    private readonly ITokenManager _tokenManager;
    
    public AuthController(IMediator mediator, ISecurityService securityService, 
        ITokenManager tokenManager)
    {
        _mediator = mediator;
        _securityService = securityService;
        _tokenManager = tokenManager;
    }
    
    /// <summary>
    ///     Авторизация юзера через логин пароль / токены
    ///     Если нет токенов, то их не передавать 
    /// </summary>
    /// <param name="request">Авторизационные данные</param>
    /// <param name="token"></param>
    /// <returns>jwt refresh tokens</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]UserLoginRequest request, CancellationToken token)
    {
        User? user;
        if (request.Token is not null && request.RefreshToken is not null)
        {
            var email = _tokenManager.GetPrincipalFromExpiredToken(request.Token)
                .FindFirstValue(ClaimTypes.Email);
            user = await _mediator.Send(new GetUserByEmail(email ?? string.Empty), token);
            if (user is null || user.RefreshToken != request.RefreshToken)
                return BadRequest("Введён некорректный Refresh токен");
        }
        else if (request.Email is not null && request.Password is not null)
        {
            user = await _mediator.Send(new LoginUser(request), token);
        }
        else
        {
            return BadRequest("Необходимо заполнить данные авторизации");
        }

        var expiresAt = DateTime.Now.AddHours(2);
        var refreshToken = _tokenManager.GenerateRefreshToken();
        var jwtToken = _tokenManager.GenerateToken(user);
        await _mediator.Send(new UpdateUserRefreshToken(user.Id!.Value, refreshToken), token);
        return Ok(new UserLoginResponse(jwtToken, refreshToken, expiresAt));
    }
    
    #region SwaggerDoc

    /// <summary>
    ///     Получить данные о текущем пользователе.
    /// </summary>
    /// <response code="200">Информация о текущем пользователе</response>
    /// <response code="400">Ошибка</response>

    #endregion

    [Authorize]
    [HttpGet("get-info")]
    public async Task<IActionResult> GetInfo(CancellationToken token)
    {
        var user = _securityService.GetCurrentUser();
        if (user is null)
            throw new UserFriendlyException("Ошибка наличия пользователя");
        
        var userDb = await _mediator.Send(
            new GetUserById(new Guid(user.FindFirstValue(ClaimTypes.Sid)!)), token);

        return Ok(userDb.Adapt<UserResponse>());
    }
}
