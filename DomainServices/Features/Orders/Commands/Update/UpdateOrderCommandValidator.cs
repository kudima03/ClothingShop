using FluentValidation;

namespace DomainServices.Features.Orders.Commands.Update;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.OrderId)} out of possible range.");

        RuleFor(x => x.ProductOptionsIdsAndQuantity)
            .NotNull();

        RuleForEach(x => x.ProductOptionsIdsAndQuantity)
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