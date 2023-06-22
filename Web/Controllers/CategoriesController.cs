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
        GetAllCategoriesQuery query = new GetAllCategoriesQuery();
        IEnumerable<Category> categories = await _mediator.Send(query);
        return Ok(categories);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Category>> GetById([FromRoute] long id)
    {
        GetCategoryByIdQuery query = new GetCategoryByIdQuery(id);
        Category category = await _mediator.Send(query);
        return Ok(category);
    }

    [HttpPost]
    [Authorize(Policy = PolicyName.Administrator)]
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
    [Authorize(Policy = PolicyName.Administrator)]
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
    [Authorize(Policy = PolicyName.Administrator)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id)
    {
        DeleteCategoryCommand command = new DeleteCategoryCommand(id);
        await _mediator.Send(command);
        return Ok();
    }
}