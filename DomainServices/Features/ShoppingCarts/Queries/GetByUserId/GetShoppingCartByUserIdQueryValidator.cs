using FluentValidation;

namespace DomainServices.Features.ShoppingCarts.Queries.GetByUserId;
public class GetShoppingCartByUserIdQueryValidator : AbstractValidator<GetShoppingCartByUserIdQuery>
{
    public GetShoppingCartByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.UserId)} out of possible range.");
    }
}