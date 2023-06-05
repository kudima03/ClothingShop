using FluentValidation;

namespace DomainServices.Features.Subcategories.Commands.Delete;

public class DeleteSubcategoryCommandValidator : AbstractValidator<DeleteSubcategoryCommand>
{
    public DeleteSubcategoryCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Id)} out of possible range.");
    }
}