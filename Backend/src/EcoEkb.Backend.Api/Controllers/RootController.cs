using System.Threading;
using System.Threading.Tasks;
using EcoEkb.Backend.Application.Common.AuthAttribute;
using EcoEkb.Backend.Application.Common.DTO.Admin.Requests;
using EcoEkb.Backend.Application.Handlers.Admin;
using EcoEkb.Backend.DataAccess.Domain.Enums;
using EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;
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
    private readonly ISecurityService _securityService;

    public RootController(IMediator mediator, ISecurityService securityService)
    {
        _mediator = mediator;
        _securityService = securityService;
    }

    /// <summary>
    ///     Позволяет создать пользователя с правами Admin
    ///     Phone можно не передавать
    /// </summary>
    /// <param name="request">Данные об админе</param>
    /// <returns></returns>

    [Authorize]
    [HasRoles(Role.Root)]
    [HttpPut("add-admin")]
    public async Task<IActionResult> AddAdmin([FromForm]AdminAddRequest request,
        CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _mediator.Send(new AddAdmin(request), token);

        return Ok();
    }
}