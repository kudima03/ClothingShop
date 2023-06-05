using ApplicationCore.Entities;
using FluentValidation;

namespace DomainServices.Features.Categories.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Category)
            .NotNull()
            .WithMessage("Object cannot be null");
        RuleFor(x => x.Category.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Category.Name)} cannot be null or empty");

        RuleFor(x => x.Category.Sections)
            .Empty()
            .WithMessage(x => $"{nameof(x.Category.Sections)} must be empty. Update in appropriate endpoint.");

        RuleFor(x => x.Category.Subcategories)
            .Must(subcategories =>
                subcategories.Count == subcategories.DistinctBy(subcategory => subcategory.Id).Count())
            .WithMessage(x => $"{nameof(x.Category.Subcategories)} must be unique.");
    }
}