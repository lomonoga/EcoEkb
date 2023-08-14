using EcoNotifications.Backend.Application.Common.DTO.Requests;
using EcoNotifications.Backend.Application.Common.DTO.Responses;
using EcoNotifications.Backend.Application.Handlers.Petition;
using EcoNotifications.Backend.DataAccess.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoNotifications.Backend.Api.Controllers;

[Produces("application/json")]
[ApiController]
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
        PetitionResponse petition;
        try
        {
            petition = await _mediator.Send(new AddPetition(petitionFormRequest), token);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

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
        
        List<PetitionResponse> petition;
        try
        {
            petition = await _mediator.Send(new GetAllPetition(), token);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

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
        PetitionResponse petition;
        try
        {
            petition = await _mediator.Send(new GetWithIdPetition(id), token);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

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
        PetitionResponse updatedPetition;
        try
        {
            updatedPetition = await _mediator.Send(new UpdatePetition(request), token);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    
        return Ok(updatedPetition);
    }
}