using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Products.Commands.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IRepository<Brand> _brandsRepository;
    private readonly IRepository<ProductColor> _productColorsRepository;
    private readonly IRepository<Product> _productsRepository;
    private readonly IRepository<Subcategory> _subcategoriesRepository;

    public CreateProductCommandHandler(IRepository<Product> productsRepository, IRepository<Brand> brandsRepository,
        IRepository<Subcategory> subcategoriesRepository, IRepository<ProductColor> productColorsRepository)
    {
        _productsRepository = productsRepository;
        _brandsRepository = brandsRepository;
        _subcategoriesRepository = subcategoriesRepository;
        _productColorsRepository = productColorsRepository;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await ValidateProductNameAsync(request.Name, cancellationToken);

        Brand brand = await ValidateAndGetBrandAsync(request.BrandId, cancellationToken);

        Subcategory subcategory = await ValidateAndGetSubcategoryAsync(request.SubcategoryId, cancellationToken);

        Product newProduct = new()
        {
            Brand = brand,
            Name = request.Name,
            ProductOptions = request.ProductOptions,
            Subcategory = subcategory
        };

        IEnumerable<ProductColor> distinctProductColors =
            request.ProductOptions.Select(x => x.ProductColor).DistinctBy(x => x.ColorHex);

        await _productColorsRepository.InsertAsync(distinctProductColors, cancellationToken);

        request.ProductOptions.ForEach(x =>
            x.ProductColor = distinctProductColors.Single(c => c.ColorHex == x.ProductColor.ColorHex));

        try
        {
            Product? product = await _productsRepository.InsertAsync(newProduct, cancellationToken);
            await _productsRepository.SaveChangesAsync(cancellationToken);
            return product;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Product)} operation. Check input.");
        }
    }

    private async Task ValidateProductNameAsync(string name, CancellationToken cancellationToken)
    {
        bool nameExists = await _productsRepository.ExistsAsync(x => x.Name == name, cancellationToken);
        if (nameExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("Product.Name", "Such product name already exists!")
            });
        }
    }

    private async Task<Brand> ValidateAndGetBrandAsync(long brandId, CancellationToken cancellationToken = default)
    {
        Brand? brand = await _brandsRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == brandId,
            cancellationToken: cancellationToken);

        if (brand is null)
        {
            throw new EntityNotFoundException($"{nameof(Brand)} with id:{brandId} doesn't exist.");
        }

        return brand;
    }

    private async Task<Subcategory> ValidateAndGetSubcategoryAsync(long subcategoryId,
        CancellationToken cancellationToken = default)
    {
        Subcategory? subcategory = await _subcategoriesRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == subcategoryId,
            cancellationToken: cancellationToken);

        if (subcategory is null)
        {
            throw new EntityNotFoundException($"{nameof(Subcategory)} with id:{subcategoryId} doesn't exist.");
        }

        return subcategory;
    }
}