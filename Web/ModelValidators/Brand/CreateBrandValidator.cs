using FluentValidation;
using Web.Features.Brands;

namespace Web.ModelValidators.Brand;

public class CreateBrandValidator : AbstractValidator<CreateBrand>
{
    public CreateBrandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Brand cannot be null.");

        RuleFor(x => x.Brand.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");

        RuleFor(x => x.Brand.Id)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage($"Id cannot be greater than {long.MaxValue}.");

        RuleFor(x => x.Brand.Name)
            .NotEmpty()
            .WithMessage("Name cannot be null or empty.");

        RuleFor(x => x.Brand.Products)
            .NotNull()
            .WithMessage("Products cannot be null.");
    }
}