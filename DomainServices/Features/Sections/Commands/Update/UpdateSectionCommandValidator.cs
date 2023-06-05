using FluentValidation;

namespace DomainServices.Features.Sections.Commands.Update;

public class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
{
    public UpdateSectionCommandValidator()
    {
        RuleFor(x => x.Section)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Section.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Section.Name)} cannot be null or empty");

        RuleForEach(x => x.Section.Categories)
            .NotNull()
            .WithMessage(x => $"{nameof(x.Section.Categories)} can't be null");
    }
}