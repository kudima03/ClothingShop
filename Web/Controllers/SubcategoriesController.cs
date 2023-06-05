using ApplicationCore.Entities;
using DomainServices.Features.Subcategories.Commands.Create;
using DomainServices.Features.Subcategories.Commands.Delete;
using DomainServices.Features.Subcategories.Commands.Update;
using DomainServices.Features.Subcategories.Queries.GetAll;
using DomainServices.Features.Subcategories.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SubcategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubcategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Subcategory>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Subcategory>>> GetAll()
    {
        IEnumerable<Subcategory> subcategories = await _mediator.Send(new GetAllSubcategoriesQuery());
        return Ok(subcategories);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(Subcategory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Subcategory>> GetById([FromRoute] long id)
    {
        Subcategory subcategory = await _mediator.Send(new GetSubcategoryByIdQuery(id));
        return Ok(subcategory);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Create([FromBody] Subcategory subcategory)
    {
        Subcategory createdCategory = await _mediator.Send(new CreateSubcategoryCommand(subcategory));
        return Ok(createdCategory.Id);
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Update([FromBody] Subcategory subcategory)
    {
        await _mediator.Send(new UpdateSubcategoryCommand(subcategory));
        return Ok();
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id)
    {
        await _mediator.Send(new DeleteSubcategoryCommand(id));
        return Ok();
    }
}