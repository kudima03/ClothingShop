using FluentValidation;

namespace DomainServices.Features.Categories.Commands.Create;

internal class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidation()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Name)} cannot be null or empty");
    }
}