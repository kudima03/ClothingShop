using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Features.Templates.BusinessRulesValidators;
using DomainServices.Features.Templates.Commands.Update;
using FluentValidation;
using FluentValidation.Results;

namespace DomainServices.Features.Products.BusinessRulesValidators;

public class UpdateProductBusinessRulesValidator : IBusinessRulesValidator<UpdateCommand<Product>>
{
    private readonly IRepository<Brand> _brandsRepository;

    private readonly IRepository<Subcategory> _subcategoriesRepository;

    public UpdateProductBusinessRulesValidator(IRepository<Brand> brandsRepository,
        IRepository<Subcategory> subcategoriesRepository)
    {
        _brandsRepository = brandsRepository;
        _subcategoriesRepository = subcategoriesRepository;
    }

    public async Task ValidateAsync(UpdateCommand<Product> entity, CancellationToken cancellation = default)
    {
        if (!await _brandsRepository.ExistsAsync(x => x.Id == entity.Entity.BrandId, cancellation))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(entity.Entity.BrandId)}",
                    $"{entity.Entity.Brand.GetType().Name} with id: {entity.Entity.BrandId} doesn't exist")
            });
        }

        if (!await _subcategoriesRepository.ExistsAsync(x => x.Id == entity.Entity.SubcategoryId, cancellation))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(entity.Entity.SubcategoryId)}",
                    $"{entity.Entity.Subcategory.GetType().Name} with id: {entity.Entity.SubcategoryId} doesn't exist")
            });
        }
    }
}