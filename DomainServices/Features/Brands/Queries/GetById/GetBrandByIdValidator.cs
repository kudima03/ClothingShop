using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;

namespace DomainServices.Features.Brands.Queries.GetById;

public class GetBrandByIdValidator : AbstractValidator<GetBrandByIdQuery>
{
    public GetBrandByIdValidator(IReadOnlyRepository<Brand> brandsRepository)
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Object must be not null");

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");

        RuleFor(x => x.Id)
            .MustAsync(async (brandId, cancellationToken) =>
                await brandsRepository.ExistsAsync(x => x.Id == brandId, cancellationToken))
            .WithMessage(x => $"Brand with id:{x.Id} doesn't exist.");
    }
}