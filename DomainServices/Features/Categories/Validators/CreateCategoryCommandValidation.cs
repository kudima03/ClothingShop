using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;

namespace DomainServices.Features.Categories.Validators;

internal class CreateCategoryCommandValidation : AbstractValidator<CreateCommand<Category>>
{
    public CreateCategoryCommandValidation()
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

        RuleFor(x => x.Entity.Sections)
            .Empty()
            .WithMessage(x =>
                $"When creating new category, {nameof(x.Entity.Sections)} must be empty. Associate in another request");

        RuleFor(x => x.Entity.Subcategories)
            .Empty()
            .WithMessage(x =>
                $"When creating new category, {nameof(x.Entity.Subcategories)} must be empty. Associate in another request");
    }
}