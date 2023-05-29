using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;

namespace DomainServices.Features.ProductsOptions.Validators;

public class CreateProductOptionCommandValidator : AbstractValidator<CreateCommand<ProductOption>>
{
    public CreateProductOptionCommandValidator()
    {
        RuleFor(x => x.Entity)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Entity.ProductId)
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.ProductId)} must be greater than 0.");

        RuleFor(x => x.Entity.ProductColorId)
            .GreaterThan(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.ProductColorId)} must be greater than 0.");

        RuleFor(x => x.Entity.Quantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.Quantity)} must be equal or greater than 0.");

        RuleFor(x => x.Entity.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage(x =>
                $"{nameof(x.Entity.Quantity)} must be equal or greater than 0.");

        RuleFor(x => x.Entity.Size)
            .NotNull()
            .NotEmpty()
            .WithMessage(x =>
                $"{nameof(x.Entity.Size)} cannot be null or empty.");
    }
}