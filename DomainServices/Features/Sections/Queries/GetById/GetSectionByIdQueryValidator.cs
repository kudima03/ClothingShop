using FluentValidation;

namespace DomainServices.Features.Sections.Queries.GetById;

public class GetSectionByIdQueryValidator : AbstractValidator<GetSectionByIdQuery>
{
    public GetSectionByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Id)} out of possible range.");
    }
}