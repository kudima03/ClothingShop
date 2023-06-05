using FluentValidation;

namespace DomainServices.Features.ProductsOptions.Commands.Update;

public class UpdateProductOptionCommandValidator : AbstractValidator<UpdateProductOptionCommand>
{
    public UpdateProductOptionCommandValidator()
    {
        RuleFor(x => x.ProductOption)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.ProductOption.ProductId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.ProductOption.ProductId)} out of possible range.");

        RuleFor(x => x.ProductOption.ProductColorId)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.ProductOption.ProductColorId)} out of possible range.");

        RuleFor(x => x.ProductOption.Quantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage(x => $"{nameof(x.ProductOption.Quantity)} out of possible range.");

        RuleFor(x => x.ProductOption.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage(x => $"{nameof(x.ProductOption.Price)} out of possible range.");

        RuleFor(x => x.ProductOption.Size)
            .NotNull()
            .NotEmpty()
            .WithMessage(x =>
                $"{nameof(x.ProductOption.Size)} cannot be null or empty.");
    }
}