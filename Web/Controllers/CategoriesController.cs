using ApplicationCore.Entities;
using DomainServices.Features.Categories.Commands.Create;
using DomainServices.Features.Categories.Commands.Delete;
using DomainServices.Features.Categories.Commands.Update;
using DomainServices.Features.Categories.Queries.GetAll;
using DomainServices.Features.Categories.Queries.GetById;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = PolicyName.Customer)]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllCategoriesQuery query = new GetAllCategoriesQuery();
        IEnumerable<Category> categories = await mediator.Send(query, cancellationToken);

        return Ok(categories);
    }

    [HttpGet("{id:required}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Category>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetCategoryByIdQuery query = new GetCategoryByIdQuery(id);
        Category category = await mediator.Send(query, cancellationToken);

        return Ok(category);
    }

    [HttpPost]
    [Authorize(Policy = PolicyName.Administrator)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Create([FromBody] CreateCategoryCommand createCommand, CancellationToken cancellationToken)
    {
        Category createdCategory = await mediator.Send(createCommand, cancellationToken);

        return Ok(createdCategory.Id);
    }

    [HttpPut]
    [Authorize(Policy = PolicyName.Administrator)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Update([FromBody] UpdateCategoryCommand updateCommand, CancellationToken cancellationToken)
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
        DeleteCategoryCommand command = new DeleteCategoryCommand(id);
        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}