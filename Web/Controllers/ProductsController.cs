﻿using ApplicationCore.Entities;
using DomainServices.Features.Products.Commands.Create;
using DomainServices.Features.Products.Commands.Delete;
using DomainServices.Features.Products.Commands.Update;
using DomainServices.Features.Products.Queries.GetAll;
using DomainServices.Features.Products.Queries.GetByBrandId;
using DomainServices.Features.Products.Queries.GetById;
using DomainServices.Features.Products.Queries.GetBySubcategoryId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        return Ok(await _mediator.Send(new GetAllProductsQuery()));
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(Brand), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Product>> GetProductById([FromRoute] long id)
    {
        return Ok(await _mediator.Send(new GetProductByIdQuery(id)));
    }

    [HttpGet("bySubcategory")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductBySubcategory([FromQuery] long subcategoryId)
    {
        return Ok(await _mediator.Send(new GetProductsBySubcategoryIdQuery(subcategoryId)));
    }

    [HttpGet("byBrand")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductByBrand([FromQuery] long brandId)
    {
        return Ok(await _mediator.Send(new GetProductsByBrandIdQuery(brandId)));
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> CreateProduct([FromBody] Product product)
    {
        await _mediator.Send(new CreateProductCommand(product));
        return Ok();
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> UpdateProduct([FromBody] Product product)
    {
        await _mediator.Send(new UpdateProductCommand(product));
        return Ok();
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> DeleteProduct([FromRoute] long id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return Ok();
    }
}