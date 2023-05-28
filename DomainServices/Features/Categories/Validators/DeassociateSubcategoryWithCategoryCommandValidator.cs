using DomainServices.Features.Categories.Commands.DeassociateSubcategory;
using FluentValidation;

namespace DomainServices.Features.Categories.Validators;

public class
    DeassociateSubcategoryWithCategoryCommandValidator : AbstractValidator<DeassociateSubcategoryWithCategoryCommand>
{
    public DeassociateSubcategoryWithCategoryCommandValidator()
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