﻿using ApplicationCore.Entities;
using DomainServices.Features.Brands.Commands.Create;
using DomainServices.Features.Brands.Commands.Delete;
using DomainServices.Features.Brands.Commands.Update;
using DomainServices.Features.Brands.Queries.GetAll;
using DomainServices.Features.Brands.Queries.GetById;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandsController : ControllerBase
{
    private readonly ILogger<BrandsController> _logger;
    private readonly IMediator _mediator;

    public BrandsController(IMediator mediator, ILogger<BrandsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetAllBrands()
    {
        try
        {
            return Ok(await _mediator.Send(new GetAllBrandsQuery()));
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetBrandById([FromRoute] long id)
    {
        try
        {
            return Ok(await _mediator.Send(new GetBrandByIdQuery(id)));
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> CreateBrand([FromBody] Brand brand)
    {
        try
        {
            brand.Id = 0;
            await _mediator.Send(new CreateBrandCommand(brand));
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> UpdateBrand([FromBody] Brand brand)
    {
        try
        {
            await _mediator.Send(new UpdateBrandCommand(brand));
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> DeleteBrand([FromRoute] long id)
    {
        try
        {
            await _mediator.Send(new DeleteBrandCommand(id));
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
}