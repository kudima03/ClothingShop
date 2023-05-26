using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;

namespace DomainServices.Features.Categories.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator(IReadOnlyRepository<Category> categoryRepository)
    {
        RuleFor(x => x.Category)
            .NotNull()
            .WithMessage("Object cannot be null");
        RuleFor(x => x.Category.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Message cannot be null or empty");

        RuleForEach(x => x.Category.SectionsBelongsTo)
            .NotNull().WithMessage("Section, category belongs to, can't be null");

        RuleFor(x => x.Category)
            .Must(category => category.SectionsBelongsTo.Count == category.SectionsBelongsTo
                .DistinctBy(section => section.Id).Count())
            .WithMessage("Sections category belongs to cannot contain duplicates.");

        RuleFor(x => x.Category.SectionsBelongsTo)
            .NotNull()
            .NotEmpty()
            .ForEach(coll =>
                coll.MustAsync(async (sectionCategoryBelongsTo, cancellationToken) =>
                    await categoryRepository.ExistsAsync(category =>
                        category.Id == sectionCategoryBelongsTo.Id, cancellationToken)))
            .WithMessage("One of sections category belongs to doesn't exist.");

        RuleFor(x => x.Category.Subcategories)
            .Empty()
            .WithMessage(
                "When creating new category, subcategories must be empty. Add subcategories in another request");
    }
}