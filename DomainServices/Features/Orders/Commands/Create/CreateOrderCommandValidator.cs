using FluentValidation;

namespace DomainServices.Features.Orders.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.UserId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.UserId)} out of possible range.");

        RuleFor(x => x.ShoppingCartItemsIds)
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.ShoppingCartItemsIds)} can't be empty");

        RuleForEach(x => x.ShoppingCartItemsIds)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.UserId)} out of possible range.");
    }
}