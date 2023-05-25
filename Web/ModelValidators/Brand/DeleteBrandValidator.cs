using FluentValidation;
using Web.Features.Brands;

namespace Web.ModelValidators.Brand;

public class DeleteBrandValidator : AbstractValidator<DeleteBrand>
{
    public DeleteBrandValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Object must be not null");

        RuleFor(x => x.BrandId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");
    }
}