using ApplicationCore.Entities;
using FluentValidation;

namespace DomainServices.Features.Colors.Commands.Update;

public class UpdateColorCommandValidator : AbstractValidator<UpdateColorCommand>
{
    public UpdateColorCommandValidator()
    {
        RuleFor(x => x.Color)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Color.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Color.Name)} cannot be null or empty");

        RuleFor(x => x.Color.Hex)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Color.Hex)} cannot be null or empty");

        RuleFor(x => x.Color.ProductColors)
            .Empty()
            .WithMessage(x =>
                $"When updating {x.Color.GetType().Name},{nameof(x.Color.ProductColors)} must be empty.");
    }
}