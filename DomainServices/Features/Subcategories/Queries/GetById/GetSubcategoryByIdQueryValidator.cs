using FluentValidation;

namespace DomainServices.Features.Subcategories.Queries.GetById;

public class GetSubcategoryByIdQueryValidator : AbstractValidator<GetSubcategoryByIdQuery>
{
    public GetSubcategoryByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Id)} out of possible range.");
    }
}