using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;

namespace DomainServices.Features.Brands.Commands.Delete;

public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
{
    public DeleteBrandCommandValidator(IReadOnlyRepository<Brand> brandsRepository)
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Object must be not null");

        RuleFor(x => x.BrandId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");

        RuleFor(x => x.BrandId)
            .MustAsync(async (brandId, cancellationToken) =>
                await brandsRepository.ExistsAsync(x => x.Id == brandId, cancellationToken))
            .WithMessage(x => $"Brand with id:{x.BrandId} doesn't exist.");
    }
}