using FluentValidation;

namespace DomainServices.Features.Categories.Commands.Create;

internal class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidation()
    {
        RuleFor(x => x.Category)
            .NotNull()
            .WithMessage("Object cannot be null");
        RuleFor(x => x.Category.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Category.Name)} cannot be null or empty");

        RuleFor(x => x.Category.Sections)
            .Empty()
            .WithMessage(x => $"{nameof(x.Category.Sections)} must be empty. Create in appropriate endpoint.");
    }
}