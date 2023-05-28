using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Update;
using FluentValidation;

namespace DomainServices.Features.Subcategories.Validators;

public class UpdateSubcategoryCommandValidatorHandler : AbstractValidator<UpdateCommand<Subcategory>>
{
    public UpdateSubcategoryCommandValidatorHandler()
    {
        RuleFor(x => x.Entity)
            .NotNull()
            .WithMessage("Object cannot be null");

        RuleFor(x => x.Entity.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Entity.Name)} cannot be null or empty");

        RuleForEach(x => x.Entity.CategoriesBelongsTo)
            .NotNull()
            .WithMessage(x => $"{nameof(x.Entity.CategoriesBelongsTo)} can't be null");

        RuleFor(x => x.Entity.CategoriesBelongsTo)
            .Empty()
            .WithMessage(x =>
                $"When updating new {x.Entity.GetType().Name},{nameof(x.Entity.CategoriesBelongsTo)} must be empty. Associate in another request");

        RuleFor(x => x.Entity.Products)
            .Empty()
            .WithMessage(x =>
                $"When updating new {x.Entity.GetType().Name},{nameof(x.Entity.Products)} must be empty. Associate in another request");
    }
}