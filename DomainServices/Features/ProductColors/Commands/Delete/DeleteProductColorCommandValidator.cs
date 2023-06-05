using ApplicationCore.Entities;
using FluentValidation;

namespace DomainServices.Features.ProductColors.Commands.Delete;

public class DeleteProductColorCommandValidator : AbstractValidator<DeleteProductColorCommand>
{
    public DeleteProductColorCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Id)} out of possible range.");
    }
}