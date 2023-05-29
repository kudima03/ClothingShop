using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Features.Templates.BusinessRulesValidators;
using DomainServices.Features.Templates.Commands.Update;
using FluentValidation;
using FluentValidation.Results;

namespace DomainServices.Features.ProductColors.BusinessRulesValidators;

public class UpdateProductColorBusinessRulesValidator : IBusinessRulesValidator<UpdateCommand<ProductColor>>
{
    private readonly IRepository<Color> _colorsRepository;

    public UpdateProductColorBusinessRulesValidator(IRepository<Color> colorsRepository)
    {
        _colorsRepository = colorsRepository;
    }

    public async Task ValidateAsync(UpdateCommand<ProductColor> entity, CancellationToken cancellation = default)
    {
        if (!await _colorsRepository.ExistsAsync(x => x.Id == entity.Entity.ColorId, cancellation))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(entity.Entity.ColorId)}",
                    $"{entity.Entity.Color.GetType().Name} with id: {entity.Entity.ColorId} doesn't exist")
            });
        }
    }
}