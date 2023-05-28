using DomainServices.Features.Sections.Queries;
using FluentValidation;

namespace DomainServices.Features.Sections.Validators;

public class GetSectionByIdQueryValidator : AbstractValidator<GetSectionByIdQuery>
{
    public GetSectionByIdQueryValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Object must be not null");

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");
    }
}