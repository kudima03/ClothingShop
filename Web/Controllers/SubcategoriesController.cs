using ApplicationCore.Entities;
using DomainServices.Features.Subcategories.Commands.Create;
using DomainServices.Features.Subcategories.Commands.Delete;
using DomainServices.Features.Subcategories.Commands.Update;
using DomainServices.Features.Subcategories.Queries.GetAll;
using DomainServices.Features.Subcategories.Queries.GetById;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = PolicyName.Customer, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(IEnumerable<Subcategory>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Subcategory>>> GetAll()
    {
        GetAllSubcategoriesQuery query = new GetAllSubcategoriesQuery();
        IEnumerable<Subcategory> subcategories = await _mediator.Send(query);
        return Ok(subcategories);
    }

    [HttpGet("{id:long}")]
    [Authorize(Policy = PolicyName.Customer, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(Subcategory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Subcategory>> GetById([FromRoute] long id)
    {
        GetSubcategoryByIdQuery query = new GetSubcategoryByIdQuery(id);
        Subcategory subcategory = await _mediator.Send(query);
        return Ok(subcategory);
    }

    [HttpPost]
    [Authorize(Policy = PolicyName.Administrator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Create([FromBody] CreateSubcategoryCommand createCommand)
    {
        Subcategory createdCategory = await _mediator.Send(createCommand);
        return Ok(createdCategory.Id);
    }

    [HttpPut]
    [Authorize(Policy = PolicyName.Administrator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Update([FromBody] UpdateSubcategoryCommand updateCommand)
    {
        await _mediator.Send(updateCommand);
        return Ok();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = PolicyName.Administrator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id)
    {
        DeleteSubcategoryCommand command = new DeleteSubcategoryCommand(id);
        await _mediator.Send(command);
        return Ok();
    }
}