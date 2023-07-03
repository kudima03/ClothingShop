using FluentValidation;

namespace DomainServices.Features.ShoppingCartItems.Commands.Delete;

public class DeleteAndReturnToStockShoppingCartItemCommandValidator
    : AbstractValidator<DeleteAndReturnToStockShoppingCartItemCommand>
{
    public DeleteAndReturnToStockShoppingCartItemCommandValidator()
    {
        RuleFor(x => x.ShoppingCartItemId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.ShoppingCartItemId)} out of possible range.");
    }
}