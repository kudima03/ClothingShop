using DomainServices.Features.Products.Queries;
using FluentValidation;

namespace DomainServices.Features.Products.Validators;

public class GetProductByBrandIdQueryValidator : AbstractValidator<GetProductsByBrandIdQuery>
{
    public GetProductByBrandIdQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.BrandId)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.BrandId)} must be greater than 0.");
    }
}