using ApplicationCore.Entities;
using DomainServices.Features.Subcategories.Commands.Create;
using DomainServices.Features.Subcategories.Commands.Delete;
using DomainServices.Features.Subcategories.Commands.Update;
using DomainServices.Features.Subcategories.Queries.GetAll;
using DomainServices.Features.Subcategories.Queries.GetById;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = PolicyName.Customer)]
public class SubcategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Subcategory>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Subcategory>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllSubcategoriesQuery query = new GetAllSubcategoriesQuery();
        IEnumerable<Subcategory> subcategories = await mediator.Send(query, cancellationToken);

        return Ok(subcategories);
    }

    [HttpGet("{id:required}")]
    [ProducesResponseType(typeof(Subcategory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Subcategory>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetSubcategoryByIdQuery query = new GetSubcategoryByIdQuery(id);
        Subcategory subcategory = await mediator.Send(query, cancellationToken);

        return Ok(subcategory);
    }

    [HttpPost]
    [Authorize(Policy = PolicyName.Administrator)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Create([FromBody] CreateSubcategoryCommand createCommand, CancellationToken cancellationToken)
    {
        Subcategory createdCategory = await mediator.Send(createCommand, cancellationToken);

        return Ok(createdCategory.Id);
    }

    [HttpPut]
    [Authorize(Policy = PolicyName.Administrator)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Update([FromBody] UpdateSubcategoryCommand updateCommand, CancellationToken cancellationToken)
    {
        await mediator.Send(updateCommand, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id:required}")]
    [Authorize(Policy = PolicyName.Administrator)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        DeleteSubcategoryCommand command = new DeleteSubcategoryCommand(id);
        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}