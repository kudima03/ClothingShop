using ApplicationCore.Entities;
using AutoMapper;
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
using Web.DTOs;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        GetAllProductsQuery query = new GetAllProductsQuery();
        IEnumerable<Product> products = await _mediator.Send(query);
        return Ok(products);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(Brand), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Product>> GetProductById([FromRoute] long id)
    {
        GetProductByIdQuery query = new GetProductByIdQuery(id);
        Product product = await _mediator.Send(query);
        return Ok(product);
    }

    [HttpGet("bySubcategory")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductBySubcategory([FromQuery] long subcategoryId)
    {
        GetProductsBySubcategoryIdQuery query = new GetProductsBySubcategoryIdQuery(subcategoryId);
        IEnumerable<Product> product = await _mediator.Send(query);
        return Ok(product);
    }

    [HttpGet("byBrand")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductByBrand([FromQuery] long brandId)
    {
        GetProductsByBrandIdQuery query = new GetProductsByBrandIdQuery(brandId);
        IEnumerable<Product> product = await _mediator.Send(query);
        return Ok(product);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> CreateProduct([FromBody] ProductDto product)
    {
        CreateProductCommand createCommand = _mapper.Map<CreateProductCommand>(product);
        Product createdProduct = await _mediator.Send(createCommand);
        return Ok(createdProduct.Id);
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> UpdateProduct([FromBody] ProductDto product)
    {
        UpdateProductCommand updateCommand = _mapper.Map<UpdateProductCommand>(product);
        await _mediator.Send(updateCommand);
        return Ok();
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> DeleteProduct([FromRoute] long id)
    {
        DeleteProductCommand command = new DeleteProductCommand(id);
        await _mediator.Send(command);
        return Ok();
    }
}