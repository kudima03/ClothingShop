using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;

namespace DomainServices.Features.Colors.Validators;

public class CreateColorCommandValidator : AbstractValidator<CreateCommand<Color>>
{
    public CreateColorCommandValidator()
    {
        RuleFor(x => x.Entity)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Entity.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Entity.Name)} cannot be null or empty");

        RuleFor(x => x.Entity.Hex)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Entity.Hex)} cannot be null or empty");

        RuleFor(x => x.Entity.ProductColors)
            .Empty()
            .WithMessage(x =>
                $"When creating new {x.Entity.GetType().Name},{nameof(x.Entity.ProductColors)} must be empty. Associate in another request");
    }
}