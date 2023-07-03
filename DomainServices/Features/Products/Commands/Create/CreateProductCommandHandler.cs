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

    public CreateProductCommandHandler(IRepository<Product> productsRepository,
                                       IRepository<Brand> brandsRepository,
                                       IRepository<Subcategory> subcategoriesRepository,
                                       IRepository<ProductColor> productColorsRepository)
    {
        _productsRepository = productsRepository;
        _brandsRepository = brandsRepository;
        _subcategoriesRepository = subcategoriesRepository;
        _productColorsRepository = productColorsRepository;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await ValidateProductNameAsync(request.Name, cancellationToken);

        await ValidateBrandAsync(request.BrandId, cancellationToken);

        await ValidateSubcategoryAsync(request.SubcategoryId, cancellationToken);

        List<ProductOption> productOptions = MapToProductOptions(request.ProductOptionsDtos);

        Product newProduct = new Product
        {
            BrandId = request.BrandId,
            Name = request.Name,
            SubcategoryId = request.SubcategoryId,
            ProductOptions = productOptions
        };

        IEnumerable<ProductColor> distinctProductColors =
            newProduct.ProductOptions.Select(x => x.ProductColor).DistinctBy(x => x.ColorHex);

        await _productColorsRepository.InsertAsync(distinctProductColors, cancellationToken);

        foreach (ProductOption item in newProduct.ProductOptions)
        {
            item.ProductColor = distinctProductColors.Single(c => c.ColorHex == item.ProductColor.ColorHex);
        }

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

    private List<ProductOption> MapToProductOptions(
        IEnumerable<CreateUpdateProductCommandsDtos.ProductOptionDto> productOptionDtos)
    {
        return productOptionDtos.Select
                                    (x => new ProductOption
                                    {
                                        Id = x.Id,
                                        Price = x.Price,
                                        Quantity = x.Quantity,
                                        Size = x.Size,
                                        ProductColorId = x.ProductColorId,
                                        ProductColor = new ProductColor
                                        {
                                            Id = x.ProductColor.Id,
                                            ColorHex = x.ProductColor.ColorHex,
                                            ImagesInfos = x.ProductColor.ImagesInfos.Select
                                                               (c => new ImageInfo
                                                               {
                                                                   Id = c.Id,
                                                                   ProductColorId = x.ProductColorId,
                                                                   Url = c.Url
                                                               })
                                                           .ToList()
                                        }
                                    })
                                .ToList();
    }

    private async Task ValidateProductNameAsync(string name, CancellationToken cancellationToken)
    {
        bool nameExists = await _productsRepository.ExistsAsync(x => x.Name == name, cancellationToken);

        if (nameExists)
        {
            throw new ValidationException
                (new[]
                {
                    new ValidationFailure("Product.Name", "Such product name already exists!")
                });
        }
    }

    private async Task ValidateBrandAsync(long brandId, CancellationToken cancellationToken = default)
    {
        bool brandExists = await _brandsRepository.ExistsAsync(x => x.Id == brandId, cancellationToken);

        if (!brandExists)
        {
            throw new EntityNotFoundException($"{nameof(Brand)} with id:{brandId} doesn't exist.");
        }
    }

    private async Task ValidateSubcategoryAsync(long subcategoryId,
                                                CancellationToken cancellationToken = default)
    {
        bool subcategoryExists = await _subcategoriesRepository.ExistsAsync(x => x.Id == subcategoryId, cancellationToken);

        if (!subcategoryExists)
        {
            throw new EntityNotFoundException($"{nameof(Subcategory)} with id:{subcategoryId} doesn't exist.");
        }
    }
}