using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Features.Templates.BusinessRulesValidators;
using DomainServices.Features.Templates.Commands.Update;
using FluentValidation;
using FluentValidation.Results;

namespace DomainServices.Features.ProductsOptions.BusinessRulesValidators;

public class UpdateProductOptionBusinessRulesValidator : IBusinessRulesValidator<UpdateCommand<ProductOption>>
{
    private readonly IRepository<ProductColor> _productColorsRepository;
    private readonly IRepository<Product> _productsRepository;

    public UpdateProductOptionBusinessRulesValidator(IRepository<Product> productsRepository,
        IRepository<ProductColor> productColorsRepository)
    {
        _productsRepository = productsRepository;
        _productColorsRepository = productColorsRepository;
    }

    public async Task ValidateAsync(UpdateCommand<ProductOption> entity, CancellationToken cancellation = default)
    {
        if (!await _productsRepository.ExistsAsync(x => x.Id == entity.Entity.ProductId, cancellation))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(entity.Entity.ProductId)}",
                    $"{entity.Entity.Product.GetType().Name} with id: {entity.Entity.ProductId} doesn't exist")
            });
        }

        if (!await _productColorsRepository.ExistsAsync(x => x.Id == entity.Entity.ProductColorId, cancellation))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(entity.Entity.ProductColorId)}",
                    $"{entity.Entity.ProductColor.GetType().Name} with id: {entity.Entity.ProductColorId} doesn't exist")
            });
        }
    }
}