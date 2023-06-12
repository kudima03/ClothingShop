using FluentValidation;

namespace DomainServices.Features.Sections.Commands.Update;

public class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
{
    public UpdateSectionCommandValidator()
    {
        RuleFor(x => x.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Id)} out of possible range.");

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Name)} cannot be null or empty");
    }
}