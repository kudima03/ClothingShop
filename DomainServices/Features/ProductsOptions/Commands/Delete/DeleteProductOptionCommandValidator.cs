using FluentValidation;

namespace DomainServices.Features.ProductsOptions.Commands.Delete;

public class DeleteProductOptionCommandValidator : AbstractValidator<DeleteProductOptionCommand>
{
    public DeleteProductOptionCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Id)} out of possible range.");
    }
}