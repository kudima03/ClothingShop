using ApplicationCore.Entities;
using FluentValidation;

namespace DomainServices.Features.Subcategories.Commands.Create;

public class CreateSubcategoryCommandValidator : AbstractValidator<CreateSubcategoryCommand>
{
    public CreateSubcategoryCommandValidator()
    {
        RuleFor(x => x.Subcategory)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Subcategory.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Subcategory.Name)} cannot be null or empty");

        RuleForEach(x => x.Subcategory.Categories)
            .NotNull()
            .WithMessage(x => $"{nameof(x.Subcategory.Categories)} can't be null");

        RuleFor(x => x.Subcategory.Categories)
            .Empty()
            .WithMessage(x =>
                $"When creating new {x.Subcategory.GetType().Name},{nameof(x.Subcategory.Categories)} must be empty. Associate in another request");

        RuleFor(x => x.Subcategory.Products)
            .Empty()
            .WithMessage(x =>
                $"When creating new {x.Subcategory.GetType().Name},{nameof(x.Subcategory.Products)} must be empty. Associate in another request");
    }
}