using FluentValidation;

namespace DomainServices.Features.Products.Commands.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Product.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Product.Name)} cannot be null or empty");

        RuleFor(x => x.Product.Reviews)
            .Empty()
            .WithMessage(x =>
                $"When creating new {x.Product.GetType().Name},{nameof(x.Product.Reviews)} must be empty. Create in another request");
    }
}