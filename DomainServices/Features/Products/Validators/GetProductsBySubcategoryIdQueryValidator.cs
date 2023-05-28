using DomainServices.Features.Products.Queries;
using FluentValidation;

namespace DomainServices.Features.Products.Validators;

public class GetProductsBySubcategoryIdQueryValidator : AbstractValidator<GetProductsBySubcategoryIdQuery>
{
    public GetProductsBySubcategoryIdQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.SubcategoryId)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.SubcategoryId)} must be greater than 0.");
    }
}