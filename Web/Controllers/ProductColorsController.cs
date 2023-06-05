using ApplicationCore.Entities;
using DomainServices.Features.ProductColors.Commands.Create;
using DomainServices.Features.ProductColors.Commands.Delete;
using DomainServices.Features.ProductColors.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductColorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductColorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Create([FromBody] ProductColor productColor)
    {
        ProductColor createdProductColor = await _mediator.Send(new CreateProductColorCommand(productColor));
        return Ok(createdProductColor.Id);
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Update([FromBody] ProductColor productColor)
    {
        await _mediator.Send(new UpdateProductColorCommand(productColor));
        return Ok();
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id)
    {
        await _mediator.Send(new DeleteProductColorCommand(id));
        return Ok();
    }
}