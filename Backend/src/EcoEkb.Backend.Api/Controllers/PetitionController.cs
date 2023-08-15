using EcoEkb.Backend.Application.Common.DTO.Requests;
using EcoEkb.Backend.Application.Handlers.Petition;
using EcoEkb.Backend.Application.Handlers.Petitions;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
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
    private readonly ISecurityService _securityService;
    
    public PetitionController(IMediator mediator, ISecurityService securityService)
    {
        _mediator = mediator;
        _securityService = securityService;
    }
    
    /// <summary>
    ///     Позволяет оставить заявку
    /// </summary>
    /// <param name="petitionFormRequest">Форма с заявкой</param>
    /// <returns>Информацию об оставленной заявке</returns>
    
    [HttpPut("submit-petition")]
    public async Task<IActionResult> SubmitPetition([FromForm]PetitionFormRequest petitionFormRequest, 
        CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var petition = await _mediator.Send(new AddPetition(petitionFormRequest), token);
        
        return Ok(petition);
    }
    
    /// <summary>
    ///     Позволяет получить все заявки
    /// </summary>
    /// <returns>Информация о всех существующих заявках</returns>
    [Authorize]
    // Добавить проверку прав
    [HttpGet("get-all-petition")]
    public async Task<IActionResult> GetAllPetition(CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var petition = await _mediator.Send(new GetAllPetition(), token);
        
        return Ok(petition);
    }
    
    /// <summary>
    ///     Позволяет получить заявку по Id
    /// </summary>
    /// <param name="id">Id заявки</param>
    /// <returns>Заявка</returns>
    [Authorize]
    [HttpGet("get-with-id-petition")]
    public async Task<IActionResult> GetWithIdPetition(Guid id, CancellationToken token)
    {
        var petition = await _mediator.Send(new GetWithIdPetition(id), token);

        return Ok(petition);
    }
    
    /// <summary>
    ///     Обновляет статус заявки по Id
    /// </summary>
    /// <param name="request">Запрос с ID и статусом заявки</param>
    /// <returns>Обновленная заявка</returns>
    [Authorize]
    // Добавить проверку прав
    [HttpPost("update-status-with-id-petition")]
    public async Task<IActionResult> UpdateStatusPetition(PetitionStatusWithIdRequest request, 
        CancellationToken token)
    {
        var updatedPetition = await _mediator.Send(new UpdatePetition(request), token);
    
        return Ok(updatedPetition);
    }
}