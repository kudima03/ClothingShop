using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandsController : ControllerBase
{
    private readonly IValidator<Brand> _brandValidator;
    private readonly IRepository<Brand> _brandsRepository;

    public BrandsController(IRepository<Brand> brandsRepository, IValidator<Brand> brandValidator)
    {
        _brandsRepository = brandsRepository;
        _brandValidator = brandValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetAllBrands()
    {
        return Ok(await _brandsRepository.GetAllAsync(disableTracking: true));
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

        Brand? brand = await _brandsRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == id);

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

        _brandsRepository.Insert(brand);

        await _brandsRepository.SaveChangesAsync();

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

        if (!await _brandsRepository.ExistsAsync(x => x.Id == brand.Id))
        {
            return BadRequest($"Brand with id:{brand.Id} doesn't exist.");
        }

        _brandsRepository.Update(brand);

        await _brandsRepository.SaveChangesAsync();

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

        Brand? brand = await _brandsRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == id);

        if (brand is null)
        {
            return BadRequest($"Brand with id:{id} doesn't exist.");
        }

        _brandsRepository.Delete(brand);

        await _brandsRepository.SaveChangesAsync();

        return Ok();
    }
}