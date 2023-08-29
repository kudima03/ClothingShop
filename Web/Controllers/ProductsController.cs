using ApplicationCore.Entities;
using DomainServices.Features.Products.Commands.Create;
using DomainServices.Features.Products.Commands.Delete;
using DomainServices.Features.Products.Commands.Update;
using DomainServices.Features.Products.Queries.GetAll;
using DomainServices.Features.Products.Queries.GetByBrandId;
using DomainServices.Features.Products.Queries.GetById;
using DomainServices.Features.Products.Queries.GetBySubcategoryId;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = PolicyName.Customer)]
public class ProductsController : Controller
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(CancellationToken cancellationToken)
    {
        GetAllProductsQuery query = new GetAllProductsQuery();
        IEnumerable<Product> products = await _mediator.Send(query, cancellationToken);

        return View("Products", products);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(Brand), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Product>> GetProductById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetProductByIdQuery query = new GetProductByIdQuery(id);
        Product product = await _mediator.Send(query, cancellationToken);

        return View("Product", product);
    }

    [HttpGet("bySubcategory")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductBySubcategory(
        [FromQuery] long subcategoryId,
        CancellationToken cancellationToken)
    {
        GetProductsBySubcategoryIdQuery query = new GetProductsBySubcategoryIdQuery(subcategoryId);
        IEnumerable<Product> product = await _mediator.Send(query, cancellationToken);

        return Ok(product);
    }

    [HttpGet("byBrand")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductByBrand([FromQuery] long brandId,
                                                                            CancellationToken cancellationToken)
    {
        GetProductsByBrandIdQuery query = new GetProductsByBrandIdQuery(brandId);
        IEnumerable<Product> product = await _mediator.Send(query, cancellationToken);

        return Ok(product);
    }

    [HttpPost]
    [Authorize(Policy = PolicyName.Administrator)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProductCommand createCommand,
                                                  CancellationToken cancellationToken)
    {
        Product createdProduct = await _mediator.Send(createCommand, cancellationToken);

        return Ok(createdProduct.Id);
    }

    [HttpPut]
    [Authorize(Policy = PolicyName.Administrator)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommand updateCommand,
                                                  CancellationToken cancellationToken)
    {
        await _mediator.Send(updateCommand, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = PolicyName.Administrator)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> DeleteProduct([FromRoute] long id, CancellationToken cancellationToken)
    {
        DeleteProductCommand command = new DeleteProductCommand(id);
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}