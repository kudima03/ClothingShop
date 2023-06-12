using ApplicationCore.Entities;
using DomainServices.Features.Categories.Commands.Create;
using DomainServices.Features.Categories.Commands.Delete;
using DomainServices.Features.Categories.Commands.Update;
using DomainServices.Features.Categories.Queries.GetAll;
using DomainServices.Features.Categories.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        IEnumerable<Category> categories = await _mediator.Send(new GetAllCategoriesQuery());
        return Ok(categories);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Category>> GetById([FromRoute] long id)
    {
        Category category = await _mediator.Send(new GetCategoryByIdQuery(id));
        return Ok(category);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Create([FromBody] CreateCategoryCommand createCommand)
    {
        Category createdCategory = await _mediator.Send(createCommand);
        return Ok(createdCategory.Id);
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Update([FromBody] UpdateCategoryCommand updateCommand)
    {
        await _mediator.Send(updateCommand);
        return Ok();
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id)
    {
        await _mediator.Send(new DeleteCategoryCommand(id));
        return Ok();
    }
}