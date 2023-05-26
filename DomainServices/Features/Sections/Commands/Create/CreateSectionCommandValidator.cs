using FluentValidation;

namespace DomainServices.Features.Sections.Commands.Create;

public class CreateSectionCommandValidator : AbstractValidator<CreateSectionCommand>
{
    public CreateSectionCommandValidator()
    {
        RuleFor(x => x.Section)
            .NotNull()
            .WithMessage("Object cannot be null");
        RuleFor(x => x.Section.Id)
            .Equal(0)
            .WithMessage("Id must equal 0");
        RuleFor(x => x.Section.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name must not be null or empty");

        RuleFor(x => x.Section.Categories)
            .Empty()
            .WithMessage(
                "When creating new section, categories must be empty. Add categories in another request");
    }
}