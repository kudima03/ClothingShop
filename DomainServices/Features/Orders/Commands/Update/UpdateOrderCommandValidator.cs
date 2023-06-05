using ApplicationCore.Entities;
using FluentValidation;

namespace DomainServices.Features.Orders.Commands.Update;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Order.OrderStatusId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Order.OrderStatusId)} out of possible range.");

        RuleFor(x => x.Order.UserId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Order.UserId)} out of possible range.");
    }
}