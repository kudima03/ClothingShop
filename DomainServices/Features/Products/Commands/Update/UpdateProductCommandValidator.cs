using ApplicationCore.Entities;
using FluentValidation;

namespace DomainServices.Features.Products.Commands.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Product.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Product.Name)} cannot be null or empty");

        RuleFor(x => x.Product.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Product.Id)} out of possible range.");

        RuleFor(x => x.Product.SubcategoryId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Product.SubcategoryId)} out of possible range.");

        RuleFor(x => x.Product.BrandId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Product.BrandId)} out of possible range.");

        RuleFor(x => x.Product.Reviews)
            .Empty()
            .WithMessage(x =>
                $"When updating {x.Product.GetType().Name},{nameof(x.Product.Reviews)} must be empty. Update in appropriate endpoint");

        RuleForEach(x => x.Product.ProductOptions)
            .ChildRules(c =>
            {
                c.RuleFor(v => v.Orders).Empty();
                c.RuleFor(v => v.Price).GreaterThanOrEqualTo(0);
                c.RuleFor(v => v.Quantity).GreaterThanOrEqualTo(0);
                c.RuleFor(v => v.Size).NotNull().NotEmpty();
            });
    }
}