using EcoNotifications.Backend.Application.Common.DTO;
using EcoNotifications.Backend.Application.Common.DTO.Responses;
using EcoNotifications.Backend.Application.Handlers.Users;
using EcoNotifications.Backend.DataAccess.Models;
using EcoNotifications.Backend.DataAccess.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcoNotifications.Backend.Api.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ISecurityService _securityService;
    private readonly ITokenManager _tokenManager;

    public UserController(IMediator mediator, ISecurityService securityService, ITokenManager tokenManager)
    {
        _mediator = mediator;
        _securityService = securityService;
        _tokenManager = tokenManager;
    }

    #region Swagger
    
    /// <summary>
    ///     Позволяет добавить пользователя
    ///     Телефон в формате 7xxx или 8xxx без "+" !!!
    /// </summary>
    /// <param name="userSaveRequest">Данные о пользователе</param>
    /// <response code="200">Добавление прошло успешно</response>
    /// <response code="400">Такой пользователь уже зарегистрирован</response>
    
    #endregion
    
    [HttpPut("add-user")]
    public async Task<IActionResult> AddUser([FromBody]UserSaveRequest userSaveRequest, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            await _mediator.Send(new SaveUser(userSaveRequest), token);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]UserLoginRequest userLoginRequest, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        User user; 
        try
        {
            user = await _mediator.Send(new LoginUser(userLoginRequest), token);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        var jwtToken = _tokenManager.GenerateToken(user);
        
        return Ok(new UserLoginResponse(jwtToken, ""));
    }
}