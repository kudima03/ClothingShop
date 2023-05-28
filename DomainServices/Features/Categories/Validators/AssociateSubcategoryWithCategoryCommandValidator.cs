using DomainServices.Features.Categories.Commands.AssociateSubcategory;
using FluentValidation;

namespace DomainServices.Features.Categories.Validators;

public class
    AssociateSubcategoryWithCategoryCommandValidator : AbstractValidator<AssociateSubcategoryWithCategoryCommand>
{
    public AssociateSubcategoryWithCategoryCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage(x => "Object must be not null");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.CategoryId)} must be greater than 0");

        RuleFor(x => x.SubcategoryId)
            .GreaterThan(0)
            .WithMessage(x => $"{nameof(x.SubcategoryId)} must be greater than 0");
    }
}