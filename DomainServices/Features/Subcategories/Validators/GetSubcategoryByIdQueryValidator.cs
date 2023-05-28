using DomainServices.Features.Subcategories.Queries;
using FluentValidation;

namespace DomainServices.Features.Subcategories.Validators;

public class GetSubcategoryByIdQueryValidator : AbstractValidator<GetSubcategoryByIdQuery>
{
    public GetSubcategoryByIdQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.Id)} must be greater than 0");
    }
}