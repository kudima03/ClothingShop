using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Features.Templates.BusinessRulesValidators;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;
using FluentValidation.Results;

namespace DomainServices.Features.ImagesInfos.BusinessRulesValidators;

public class CreateImageInfoBusinessRulesValidator : IBusinessRulesValidator<CreateCommand<ImageInfo>>
{
    private readonly IRepository<ProductColor> _productColorsRepository;

    public CreateImageInfoBusinessRulesValidator(IRepository<ProductColor> productColorsRepository)
    {
        _productColorsRepository = productColorsRepository;
    }

    public async Task ValidateAsync(CreateCommand<ImageInfo> entity, CancellationToken cancellation = default)
    {
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