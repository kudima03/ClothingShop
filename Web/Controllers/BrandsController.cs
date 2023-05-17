using ApplicationCore.Entities;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandsController : ControllerBase
{
    private readonly IValidator<Brand> _brandValidator;
    private readonly ShopContext _shopContext;

    public BrandsController(ShopContext shopContext, IValidator<Brand> brandValidator)
    {
        _shopContext = shopContext;
        _brandValidator = brandValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetAllBrands()
    {
        return Ok(await _shopContext.Brands.AsNoTracking().ToListAsync());
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

        Brand? brand = await _shopContext.Brands.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

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

        await _shopContext.Brands.AddAsync(brand);

        await _shopContext.SaveChangesAsync();

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

        if (!await _shopContext.Brands.ContainsAsync(brand))
        {
            return BadRequest($"Brand with id:{brand.Id} doesn't exist.");
        }

        _shopContext.Brands.Update(brand);

        await _shopContext.SaveChangesAsync();

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

        Brand? brand = await _shopContext.Brands.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

        if (brand is null)
        {
            return BadRequest($"Brand with id:{id} doesn't exist.");
        }

        _shopContext.Brands.Remove(brand);

        await _shopContext.SaveChangesAsync();

        return Ok();
    }
}