using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Update;
using FluentValidation;

namespace DomainServices.Features.Orders.Validators;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateCommand<Order>>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Entity)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Entity.OrderStatusId)
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.OrderStatusId)} must be greater than 0.");

        RuleFor(x => x.Entity.UserId)
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.UserId)} must be greater than 0.");

        RuleFor(x => x.Entity.ProductsOptions)
            .Empty()
            .WithMessage(x =>
                $"When updating {x.Entity.GetType().Name},{nameof(x.Entity.ProductsOptions)} must be empty. Associate in another request");
    }
}