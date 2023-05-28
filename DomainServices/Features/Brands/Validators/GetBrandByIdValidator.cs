using DomainServices.Features.Brands.Queries;
using FluentValidation;

namespace DomainServices.Features.Brands.Validators;

public class GetBrandByIdValidator : AbstractValidator<GetBrandByIdQuery>
{
    public GetBrandByIdValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Object must be not null");

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");
    }
}