using ApplicationCore.Entities;
using DomainServices.Features.Brands.Commands.Create;
using DomainServices.Features.Brands.Commands.Delete;
using DomainServices.Features.Brands.Commands.Update;
using DomainServices.Features.Brands.Queries.GetAll;
using DomainServices.Features.Brands.Queries.GetById;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = PolicyName.Customer)]
public class BrandsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllBrandsQuery query = new GetAllBrandsQuery();
        IEnumerable<Brand> brands = await mediator.Send(query, cancellationToken);

        return Ok(brands);
    }

    [HttpGet("{id:required}")]
    [ProducesResponseType(typeof(Brand), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Brand>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetBrandByIdQuery query = new GetBrandByIdQuery(id);
        Brand brand = await mediator.Send(query, cancellationToken);

        return Ok(brand);
    }

    [HttpPost]
    [Authorize(Policy = PolicyName.Administrator)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Create([FromBody] CreateBrandCommand createCommand, CancellationToken cancellationToken)
    {
        Brand createdBrand = await mediator.Send(createCommand, cancellationToken);

        return Ok(createdBrand.Id);
    }

    [HttpPut]
    [Authorize(Policy = PolicyName.Administrator)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Update([FromBody] UpdateBrandCommand updateCommand, CancellationToken cancellationToken)
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
        DeleteBrandCommand command = new DeleteBrandCommand(id);
        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}