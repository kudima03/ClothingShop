using DomainServices.Features.ProductsOptions.Queries;
using FluentValidation;

namespace DomainServices.Features.ProductsOptions.Validators;

public class GetProductOptionByIdQueryValidator : AbstractValidator<GetProductOptionByIdQuery>
{
    public GetProductOptionByIdQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.Id)} must be greater than 0.");
    }
}