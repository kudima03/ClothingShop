using DomainServices.Features.ProductColors.Queries;
using FluentValidation;

namespace DomainServices.Features.ProductColors.Validators;

public class GetProductColorByIdQueryValidator : AbstractValidator<GetProductColorByIdQuery>
{
    public GetProductColorByIdQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.Id)} must be greater than 0.");
    }
}