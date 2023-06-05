using FluentValidation;

namespace DomainServices.Features.Products.Queries.GetByBrandId;

public class GetProductByBrandIdQueryValidator : AbstractValidator<GetProductsByBrandIdQuery>
{
    public GetProductByBrandIdQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.BrandId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.BrandId)} out of possible range.");
    }
}