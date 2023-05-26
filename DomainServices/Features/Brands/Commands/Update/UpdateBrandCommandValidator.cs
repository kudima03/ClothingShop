using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;

namespace DomainServices.Features.Brands.Commands.Update;

public class UpdateBrandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandValidator(IReadOnlyRepository<Brand> brandsRepository)
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Brand cannot be null.");

        RuleFor(x => x.Brand.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");

        RuleFor(x => x.Brand.Id)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage($"Id cannot be greater than {long.MaxValue}.");

        RuleFor(x => x.Brand.Name)
            .NotEmpty()
            .WithMessage("Name cannot be null or empty.");

        RuleFor(x => x.Brand.Products)
            .NotNull()
            .WithMessage("Products cannot be null.");

        RuleFor(x => x.Brand)
            .MustAsync(async (brand, cancellationToken) =>
                await brandsRepository.ExistsAsync(x => x.Id == brand.Id, cancellationToken))
            .WithMessage(x => $"Brand with id:{x.Brand.Id} doesn't exist.");
    }
}