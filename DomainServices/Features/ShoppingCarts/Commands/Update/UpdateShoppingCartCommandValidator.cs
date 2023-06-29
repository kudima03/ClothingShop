using FluentValidation;

namespace DomainServices.Features.ShoppingCarts.Commands.Update;
public class UpdateShoppingCartCommandValidator : AbstractValidator<UpdateShoppingCartCommand>
{
    public UpdateShoppingCartCommandValidator()
    {
        RuleFor(x => x.UserId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.UserId)} out of possible range.");

        RuleFor(x => x.ItemsDtos)
            .NotNull();

        RuleForEach(x => x.ItemsDtos)
            .NotNull()
            .ChildRules(c =>
            {
                c.RuleFor(v => v.ProductOptionId)
                    .InclusiveBetween(1, long.MaxValue)
                    .WithMessage("ProductOptionId id out of possible range.");

                c.RuleFor(v => v.Quantity)
                    .InclusiveBetween(1, int.MaxValue)
                    .WithMessage("Quantity out of possible range.");
            });
    }
}