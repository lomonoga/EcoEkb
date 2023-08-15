using EcoEkb.Backend.Application.Common.DTO.Requests;
using EcoEkb.Backend.Application.Handlers.Users;
using EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcoEkb.Backend.Api.Controllers;

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
    ///     Email должен быть верным!
    ///     Пароль от 10 символов!
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
        
        await _mediator.Send(new SaveUser(userSaveRequest), token);

        return Ok();
    }
}