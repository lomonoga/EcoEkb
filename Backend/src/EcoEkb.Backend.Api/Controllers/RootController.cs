using EcoEkb.Backend.Application.Common.AuthAttribute;
using EcoEkb.Backend.Application.Common.DTO.Admin.Requests;
using EcoEkb.Backend.Application.Handlers.Admin;
using EcoEkb.Backend.DataAccess.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoEkb.Backend.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class RootController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public RootController(IMediator mediator)
    {
        _mediator = mediator;
    }

    # region Swagger
    
    /// <summary>
    ///     Позволяет создать пользователя с правами Admin
    ///     Phone можно не передавать
    /// </summary>
    /// <param name="request">Данные об админе</param>
    /// <returns></returns>

    #endregion
    
    [Authorize]
    [HasRoles(Role.Root)]
    [HttpPut("add-admin")]
    public async Task<IActionResult> AddAdmin([FromBody]AdminAddRequest request,
        CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _mediator.Send(new AddAdmin(request), token);

        return Ok();
    }
    
    # region Swagger
    
    /// <summary>
    ///     Позволяет получить Admin по Id
    /// </summary>
    /// <param name="request">Id админа</param>
    /// <returns>Данные об Admin</returns>
    
    #endregion

    [Authorize]
    [HasRoles(Role.Root)]
    [HttpGet("get-admin-by-id")]
    public async Task<IActionResult> GetAdminById(Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var admin = await _mediator.Send(new GetAdminById(id), token);

        return Ok(admin);
    }
    
    # region Swagger
    
    /// <summary>
    ///     Позволяет получить всех Admin
    /// </summary>
    /// <returns>Список всех Admin</returns>
    
     #endregion

    [Authorize]
    [HasRoles(Role.Root)]
    [HttpGet("get-all-admins")]
    public async Task<IActionResult> GetAllAdmins(CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var listAdmins = await _mediator.Send(new GetAllAdmins(), token);

        return Ok(listAdmins);
    }
    
    # region Swagger
    
    /// <summary>
    ///     Позволяет удалить Admin по Id
    /// </summary>
    /// <param name="request">Id админа</param>
    /// <returns></returns>
    
    #endregion

    [Authorize]
    [HasRoles(Role.Root)]
    [HttpDelete("delete-admin-by-id")]
    public async Task<IActionResult> DeleteAdminById(Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _mediator.Send(new DeleteAdmin(id), token);

        return Ok();
    }
}