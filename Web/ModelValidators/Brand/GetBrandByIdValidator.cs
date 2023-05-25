using FluentValidation;
using Web.Features.Brands;

namespace Web.ModelValidators.Brand;

public class GetBrandByIdValidator : AbstractValidator<GetBrandById>
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