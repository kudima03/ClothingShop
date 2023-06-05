using FluentValidation;

namespace DomainServices.Features.Products.Queries.GetBySubcategoryId;

public class GetProductsBySubcategoryIdQueryValidator : AbstractValidator<GetProductsBySubcategoryIdQuery>
{
    public GetProductsBySubcategoryIdQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.SubcategoryId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.SubcategoryId)} out of possible range.");
    }
}