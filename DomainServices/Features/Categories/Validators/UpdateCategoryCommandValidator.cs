using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Update;
using FluentValidation;

namespace DomainServices.Features.Categories.Validators;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCommand<Category>>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Entity)
            .NotNull()
            .WithMessage("Object cannot be null");
        RuleFor(x => x.Entity.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Message cannot be null or empty");

        RuleForEach(x => x.Entity.Sections)
            .NotNull().WithMessage("Section, category belongs to, can't be null");

        RuleFor(x => x.Entity)
            .Must(category => category.Sections.Count == category.Sections
                .DistinctBy(section => section.Id).Count())
            .WithMessage("Sections category belongs to cannot contain duplicates.");

        RuleFor(x => x.Entity.Subcategories)
            .Empty()
            .WithMessage(
                "When creating new category, subcategories must be empty. Add subcategories in another request");
    }
}