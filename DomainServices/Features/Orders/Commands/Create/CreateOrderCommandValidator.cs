using FluentValidation;

namespace DomainServices.Features.Orders.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.UserId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.UserId)} out of possible range.");

        RuleForEach(x => x.OrderItemsDtos)
            .ChildRules(x =>
            {
                x.RuleFor(c => c.ProductOptionId)
                 .InclusiveBetween(1, long.MaxValue)
                 .WithMessage(x => $"{nameof(x.ProductOptionId)} out of possible range.");

                x.RuleFor(c => c.Quantity)
                 .InclusiveBetween(1, int.MaxValue)
                 .WithMessage(x => $"{nameof(x.Quantity)} out of possible range.");
            });
    }
}