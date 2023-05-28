using DomainServices.Features.Categories.Queries;
using FluentValidation;

namespace DomainServices.Features.Categories.Validators;

public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Object must be not null");

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");
    }
}