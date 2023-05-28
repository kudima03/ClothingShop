using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;

namespace DomainServices.Features.Sections.Validators;

public class CreateSectionCommandValidator : AbstractValidator<CreateCommand<Section>>
{
    public CreateSectionCommandValidator()
    {
        RuleFor(x => x.Entity)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Entity.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Entity.Name)} cannot be null or empty");

        RuleForEach(x => x.Entity.Categories)
            .NotNull()
            .WithMessage(x => $"{nameof(x.Entity.Categories)} can't be null");

        RuleFor(x => x.Entity.Categories)
            .Empty()
            .WithMessage(x =>
                $"When updating new {x.Entity.GetType().Name},{nameof(x.Entity.Categories)} must be empty. Associate in another request");
    }
}