﻿using ApplicationCore.Entities;
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

    public UpdateProductCommandHandler(IRepository<Product> productRepository, IRepository<Brand> brandsRepository,
        IRepository<Subcategory> subcategoriesRepository, IRepository<ProductColor> productColorsRepository,
        IRepository<ImageInfo> imageInfosRepository, IRepository<ProductOption> productOptionsRepository)
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

        Brand brand = await ValidateAndGetBrandAsync(request.BrandId, cancellationToken);

        Subcategory subcategory = await ValidateAndGetSubcategoryAsync(request.SubcategoryId, cancellationToken);

        product.Name = request.Name;
        product.Brand = brand;
        product.Subcategory = subcategory;

        await ApplyProductOptionsAsync(product, request.ProductOptions, cancellationToken);

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

    private async Task<Product> ValidateAndGetProduct(long productId, CancellationToken cancellationToken = default)
    {
        Product? product = await
            _productRepository.GetFirstOrDefaultAsync(x => x.Id == productId,
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

    private async Task DeleteUnusedProductColors(CancellationToken cancellationToken = default)
    {
        IList<ProductColor>? productColorsToRemove = await _productColorsRepository.GetAllAsync(
            predicate: x => x.ProductOptions.Count == 0,
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
            productColor.ImagesInfos.Except(modifiedImagesInfos, new ImageInfoEqualityComparer()).ToList();

        productColor.ImagesInfos.RemoveAll(imageInfo => imagesToRemove.Contains(imageInfo));

        productColor.ImagesInfos.AddRange(imagesToAdd);
    }

    private async Task CreateOrUpdateRelatedProductColors(Product product, IEnumerable<ProductOption> productOptions,
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

    private async Task ApplyProductOptionsAsync(Product product, List<ProductOption> modifiedProductOptions,
        CancellationToken cancellationToken = default)
    {
        ModifyExistingProductOptions(product.ProductOptions, modifiedProductOptions);

        IEnumerable<ProductOption> optionsToAdd = modifiedProductOptions.Where(x => x.Id == 0).ToList();

        IEnumerable<ProductOption> optionsToRemove = product.ProductOptions
            .Except(modifiedProductOptions, new ProductOptionEqualityComparer()).ToList();

        await CreateOrUpdateRelatedProductColors(product, modifiedProductOptions, cancellationToken);

        product.ProductOptions.AddRange(optionsToAdd);

        product.ProductOptions.RemoveAll(productOption => optionsToRemove.Contains(productOption));
    }

    private class ProductOptionEqualityComparer : IEqualityComparer<ProductOption>
    {
        public bool Equals(ProductOption? x, ProductOption? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.Id == y.Id;
        }

        public int GetHashCode(ProductOption obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    private class ImageInfoEqualityComparer : IEqualityComparer<ImageInfo>
    {
        public bool Equals(ImageInfo? x, ImageInfo? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.Id == y.Id;
        }

        public int GetHashCode(ImageInfo obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}