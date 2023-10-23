using ApplicationCore.Entities;
using ApplicationCore.EqualityComparers;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Products.Commands.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IRepository<Brand> _brandsRepository;
    private readonly IRepository<ProductColor> _productColorsRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Subcategory> _subcategoriesRepository;

    public UpdateProductCommandHandler(IRepository<Product> productRepository,
                                       IRepository<Brand> brandsRepository,
                                       IRepository<Subcategory> subcategoriesRepository,
                                       IRepository<ProductColor> productColorsRepository)
    {
        _productRepository = productRepository;
        _brandsRepository = brandsRepository;
        _subcategoriesRepository = subcategoriesRepository;
        _productColorsRepository = productColorsRepository;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await ValidateAndGetProduct(request.Id, cancellationToken);

        if (product.Name != request.Name)
        {
            await ValidateProductNameAsync(request.Name, cancellationToken);
        }

        await ValidateBrandAsync(request.BrandId, cancellationToken);

        await ValidateSubcategoryAsync(request.SubcategoryId, cancellationToken);

        product.Name = request.Name;
        product.BrandId = request.BrandId;
        product.SubcategoryId = request.SubcategoryId;

        List<ProductOption> productOptions = MapToProductOptions(request.ProductOptionsDtos);

        await ApplyProductOptionsAsync(product, productOptions, cancellationToken);

        try
        {
            await _productRepository.SaveChangesAsync(cancellationToken);
            await DeleteUnusedProductColors(cancellationToken);
            await _productColorsRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform update {nameof(Product)} operation. Check input.");
        }

        return Unit.Value;
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

    private async Task<Product> ValidateAndGetProduct(long productId, CancellationToken cancellationToken = default)
    {
        Product? product = await
                               _productRepository.GetFirstOrDefaultAsync
                                   (x => x.Id == productId,
                                    x => x.Include(c => c.ProductOptions)
                                          .ThenInclude(c => c.ProductColor)
                                          .ThenInclude(c => c.ImagesInfos),
                                    cancellationToken);

        if (product is null)
        {
            throw new EntityNotFoundException($"{nameof(Product)} with id:{productId} doesn't exist.");
        }

        return product;
    }

    private async Task ValidateProductNameAsync(string name, CancellationToken cancellationToken = default)
    {
        bool nameExists = await _productRepository.ExistsAsync(x => x.Name == name, cancellationToken);

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

    private async Task DeleteUnusedProductColors(CancellationToken cancellationToken = default)
    {
        IList<ProductColor>? productColorsToRemove = await _productColorsRepository.GetAllAsync
                                                         (predicate: x => x.ProductOptions.Count == 0,
                                                          cancellationToken: cancellationToken);

        _productColorsRepository.Delete(productColorsToRemove);
    }

    private void CreateOrUpdateRelatedImages(ProductColor productColor,
                                             IEnumerable<ImageInfo> modifiedImagesInfos)
    {
        foreach (ImageInfo? item in productColor.ImagesInfos)
        {
            ImageInfo? modifiedImageInfo = modifiedImagesInfos.FirstOrDefault(x => x.Id == item.Id && x.Id != 0);

            if (modifiedImageInfo is not null)
            {
                item.Url = modifiedImageInfo.Url;
            }
        }

        IEnumerable<ImageInfo> imagesToAdd =
            modifiedImagesInfos.Where(x => !productColor.ImagesInfos.Select(x => x.Id).Contains(x.Id)).ToList();

        IEnumerable<ImageInfo> imagesToRemove =
            productColor.ImagesInfos.Except(modifiedImagesInfos).ToList();

        productColor.ImagesInfos.RemoveAll(imageInfo => imagesToRemove.Contains(imageInfo));

        productColor.ImagesInfos.AddRange(imagesToAdd);
    }

    private async Task CreateOrUpdateRelatedProductColors(Product product,
                                                          IEnumerable<ProductOption> productOptions,
                                                          CancellationToken cancellationToken)
    {
        List<ProductColor> existingProductColors =
            product.ProductOptions.Select(x => x.ProductColor).DistinctBy(x => x.Id).ToList();

        foreach (ProductOption item in productOptions)
        {
            ProductColor? existingProductColor =
                existingProductColors.SingleOrDefault(x => x.ColorHex == item.ProductColor.ColorHex);

            if (existingProductColor is null)
            {
                ProductColor? newProductColor =
                    await _productColorsRepository.InsertAsync(item.ProductColor, cancellationToken);

                existingProductColors.Add(newProductColor);
                item.ProductColor = newProductColor;
            }
            else
            {
                CreateOrUpdateRelatedImages(existingProductColor, item.ProductColor.ImagesInfos);
                item.ProductColor = existingProductColor;
            }
        }
    }

    private void ModifyExistingProductOptions(IEnumerable<ProductOption> existingProductOptions,
                                              IEnumerable<ProductOption> modifiedProductOptions)
    {
        foreach (ProductOption item in existingProductOptions)
        {
            ProductOption? modifiedProductOption = modifiedProductOptions.SingleOrDefault(x => x.Id == item.Id);

            if (modifiedProductOption is not null)
            {
                item.Quantity = modifiedProductOption.Quantity;
                item.Price = modifiedProductOption.Price;
                item.Size = modifiedProductOption.Size;
            }
        }
    }

    private async Task ApplyProductOptionsAsync(Product product,
                                                ICollection<ProductOption> modifiedProductOptions,
                                                CancellationToken cancellationToken = default)
    {
        ModifyExistingProductOptions(product.ProductOptions, modifiedProductOptions);

        IEnumerable<ProductOption> optionsToAdd = modifiedProductOptions.Where(x => x.Id == 0).ToList();

        IEnumerable<ProductOption> optionsToRemove = product.ProductOptions
                                                            .Except
                                                                (modifiedProductOptions)
                                                            .ToList();

        await CreateOrUpdateRelatedProductColors(product, modifiedProductOptions, cancellationToken);

        product.ProductOptions.AddRange(optionsToAdd);

        product.ProductOptions.RemoveAll(productOption => optionsToRemove.Contains(productOption));
    }
}