using FluentValidation;

namespace DomainServices.Services.OrdersService.Queries;
public class CheckUserExistsQueryValidator : AbstractValidator<CheckUserExistsQuery>
{
    public CheckUserExistsQueryValidator()
    {
        RuleFor(x => x.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Id)} out of possible range.");
    }
}
