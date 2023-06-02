using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;

namespace DomainServices.Features.Subcategories.Validators;

public class CreateSubcategoryCommandValidator : AbstractValidator<CreateCommand<Subcategory>>
{
    public CreateSubcategoryCommandValidator()
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
                $"When creating new {x.Entity.GetType().Name},{nameof(x.Entity.Categories)} must be empty. Associate in another request");

        RuleFor(x => x.Entity.Products)
            .Empty()
            .WithMessage(x =>
                $"When creating new {x.Entity.GetType().Name},{nameof(x.Entity.Products)} must be empty. Associate in another request");
    }
}