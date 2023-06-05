using ApplicationCore.Entities;
using DomainServices.Features.ProductsOptions.Commands.Delete;
using DomainServices.Features.ProductsOptions.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsOptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsOptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Update([FromBody] ProductOption productOption)
    {
        await _mediator.Send(new UpdateProductOptionCommand(productOption));
        return Ok();
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id)
    {
        await _mediator.Send(new DeleteProductOptionCommand(id));
        return Ok();
    }
}