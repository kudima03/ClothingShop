using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;

namespace DomainServices.Features.Orders.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateCommand<Order>>
{
    public CreateOrderCommandValidator()
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
                $"When creating new {x.Entity.GetType().Name},{nameof(x.Entity.ProductsOptions)} must be empty. Associate in another request");
    }
}