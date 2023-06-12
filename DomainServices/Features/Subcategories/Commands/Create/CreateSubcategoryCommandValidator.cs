using FluentValidation;

namespace DomainServices.Features.Subcategories.Commands.Create;

public class CreateSubcategoryCommandValidator : AbstractValidator<CreateSubcategoryCommand>
{
    public CreateSubcategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Name)} cannot be null or empty");
    }
}