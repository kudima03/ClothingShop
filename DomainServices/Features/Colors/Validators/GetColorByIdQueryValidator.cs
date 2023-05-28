using DomainServices.Features.Colors.Queries;
using FluentValidation;

namespace DomainServices.Features.Colors.Validators;

public class GetColorByIdQueryValidator : AbstractValidator<GetColorByIdQuery>
{
    public GetColorByIdQueryValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.Id)} must be greater than 0.");
    }
}