using ApplicationCore.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Web.Features.Brands;

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
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetAllBrands()
    {
        return Ok(await _mediator.Send(new GetAllBrands()));
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetBrandById([FromRoute] long id)
    {
        try
        {
            Brand? brand = await _mediator.Send(new GetBrandById(id));
            if (brand is null)
            {
                return BadRequest($"Brand with id:{id} doesn't exist.");
            }

            return Ok(brand);
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
            await _mediator.Send(new CreateBrand(brand));
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
            Brand? brandToUpdate = await _mediator.Send(new GetBrandById(brand.Id));
            if (brandToUpdate is null)
            {
                return BadRequest($"Brand with id:{brand.Id} doesn't exist.");
            }

            await _mediator.Send(new UpdateBrand(brand));
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
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> DeleteBrand([FromRoute] long id)
    {
        try
        {
            Brand? brand = await _mediator.Send(new GetBrandById(id));
            if (brand is null)
            {
                return BadRequest($"Brand with id:{id} doesn't exist.");
            }

            await _mediator.Send(new DeleteBrand(id));
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