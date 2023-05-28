using DomainServices.Features.Products.Queries;
using FluentValidation;

namespace DomainServices.Features.Products.Validators;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.Id)} must be greater than 0.");
    }
}