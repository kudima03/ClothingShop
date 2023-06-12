using FluentValidation;

namespace DomainServices.Features.Brands.Queries.GetById;

public class GetBrandByIdQueryValidator : AbstractValidator<GetBrandByIdQuery>
{
    public GetBrandByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Id)} out of possible range.");
    }
}