using ApplicationCore.Entities;
using DomainServices.Features.Subcategories.Queries;
using DomainServices.Features.Templates.Commands.Create;
using DomainServices.Features.Templates.Commands.Delete;
using DomainServices.Features.Templates.Commands.Update;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SubcategoriesController : ControllerBase
{
    private readonly ILogger<SubcategoriesController> _logger;
    private readonly IMediator _mediator;

    public SubcategoriesController(IMediator mediator, ILogger<SubcategoriesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Subcategory>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Subcategory>>> GetAll()
    {
        try
        {
            return Ok(await _mediator.Send(new GetAllSubcategoriesQuery()));
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(Subcategory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Subcategory>> GetById([FromRoute] long id)
    {
        try
        {
            return Ok(await _mediator.Send(new GetSubcategoryByIdQuery(id)));
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Create([FromBody] Subcategory subcategory)
    {
        try
        {
            subcategory.Id = 0;
            await _mediator.Send(new CreateCommand<Subcategory>(subcategory));
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Update([FromBody] Subcategory subcategory)
    {
        try
        {
            await _mediator.Send(new UpdateCommand<Subcategory>(subcategory));
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id)
    {
        try
        {
            await _mediator.Send(new DeleteCommand<Subcategory>(id));
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
}