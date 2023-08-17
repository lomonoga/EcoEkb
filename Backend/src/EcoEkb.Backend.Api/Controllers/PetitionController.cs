using EcoEkb.Backend.Application.Common.AuthAttribute;
using EcoEkb.Backend.Application.Common.DTO.Petition.Requests;
using EcoEkb.Backend.Application.Common.DTO.Requests;
using EcoEkb.Backend.Application.Handlers.Petition;
using EcoEkb.Backend.Application.Handlers.Petitions;
using EcoEkb.Backend.DataAccess.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoEkb.Backend.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class PetitionController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public PetitionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    ///     Позволяет оставить заявку
    /// </summary>
    /// <param name="request">Форма с заявкой</param>
    /// <returns>Информацию об оставленной заявке</returns>
    
    [Authorize]
    [HttpPut("add-petition")]
    public async Task<IActionResult> SubmitPetition([FromForm]PetitionFormRequest request, 
        CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _mediator.Send(new AddPetition(request), token);
        
        return Ok();
    }
    
    /// <summary>
    ///     Позволяет получить все заявки
    /// </summary>
    /// <returns>Информация о всех существующих заявках</returns>
    [Authorize]
    [HasRoles(Role.Admin)]
    [HttpGet("get-all-petitions")]
    public async Task<IActionResult> GetAllPetitions(CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var petition = await _mediator.Send(new GetAllPetitions(), token);
        
        return Ok(petition);
    }
    
    /// <summary>
    ///     Позволяет получить заявку по Id
    /// </summary>
    /// <param name="id">Id заявки</param>
    /// <returns>Заявка</returns>
    [Authorize]
    [HasRoles(Role.Admin)]
    [HttpGet("get-petition-by-id")]
    public async Task<IActionResult> GetWithIdPetition(Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var petition = await _mediator.Send(new GetWithIdPetition(id), token);

        return Ok(petition);
    }
    
    /// <summary>
    ///     Обновляет статус заявки по Id
    /// </summary>
    /// <param name="request">Запрос с ID и статусом заявки</param>
    /// <returns>Обновленная заявка</returns>
    [Authorize]
    [HasRoles(Role.Admin)]
    [HttpPost("update-status-petition-by-id")]
    public async Task<IActionResult> UpdateStatusPetition(PetitionStatusWithIdRequest request, 
        CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var updatedPetition = await _mediator.Send(new UpdatePetitionStatus(request), token);
    
        return Ok(updatedPetition);
    }
    
    /// <summary>
    ///     Удаляет заявку по Id
    /// </summary>
    /// <param name="request">Запрос с ID и статусом заявки</param>
    /// <returns>Обновленная заявка</returns>
    [Authorize]
    [HasRoles(Role.Admin)]
    [HttpDelete("delete-petition-by-id")]
    public async Task<IActionResult> DeletePetition(Guid request, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _mediator.Send(new DeletePetitionWithId(request), token);
    
        return Ok();
    }
}