using FluentValidation;

namespace DomainServices.Features.Subcategories.Commands.Update;

public class UpdateSubcategoryCommandValidatorHandler : AbstractValidator<UpdateSubcategoryCommand>
{
    public UpdateSubcategoryCommandValidatorHandler()
    {
        RuleFor(x => x.Id)
            .InclusiveBetween(1, long.MaxValue)
            .WithMessage(x => $"{nameof(x.Id)} out of possible range.");

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(x => $"{nameof(x.Name)} cannot be null or empty.");

        RuleFor(x => x.CategoriesIds)
            .NotNull();

        RuleForEach(x => x.CategoriesIds)
            .NotNull()
            .ChildRules
                (c =>
                {
                    c.RuleFor(id => id)
                     .InclusiveBetween(1, long.MaxValue)
                     .WithMessage("Section id out of possible range.");
                });
    }
}