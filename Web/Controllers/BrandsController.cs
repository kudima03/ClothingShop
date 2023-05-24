using ApplicationCore.Entities;
using DomainServices.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandsController : ControllerBase
{
    private readonly IBrandsService _brandsService;
    private readonly IValidator<Brand> _brandValidator;

    public BrandsController(IBrandsService brandsService, IValidator<Brand> brandValidator)
    {
        _brandsService = brandsService;
        _brandValidator = brandValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetAllBrands()
    {
        return Ok(await _brandsService.GetAllBrandsAsync());
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetBrandById([FromRoute] long id)
    {
        if (id < 0)
        {
            return BadRequest("Id cannot be less than zero.");
        }

        Brand? brand = await _brandsService.GetBrandByIdAsync(id);

        if (brand is null)
        {
            return BadRequest($"Brand with id:{id} doesn't exist.");
        }

        return Ok(brand);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateBrand([FromBody] Brand brand)
    {
        ValidationResult validationResult = await _brandValidator.ValidateAsync(brand);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        brand.Id = 0;

        await _brandsService.CreateBrandAsync(brand);

        return Ok();
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateBrand([FromBody] Brand brand)
    {
        ValidationResult validationResult = await _brandValidator.ValidateAsync(brand);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        if (await _brandsService.GetBrandByIdAsync(brand.Id) is null)
        {
            return BadRequest($"Brand with id:{brand.Id} doesn't exist.");
        }

        await _brandsService.UpdateBrandAsync(brand);

        return Ok();
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteBrand([FromRoute] long id)
    {
        if (id < 0)
        {
            return BadRequest("Id cannot be less than zero.");
        }

        Brand? brand = await _brandsService.GetBrandByIdAsync(id);

        if (brand is null)
        {
            return BadRequest($"Brand with id:{id} doesn't exist.");
        }

        await _brandsService.DeleteBrandAsync(brand);

        return Ok();
    }
}