using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Update;
using FluentValidation;

namespace DomainServices.Features.Colors.Validators;

public class UpdateColorCommandValidator : AbstractValidator<UpdateCommand<Color>>
{
    public UpdateColorCommandValidator()
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
                $"When updating {x.Entity.GetType().Name},{nameof(x.Entity.ProductColors)} must be empty. Associate in another request");
    }
}