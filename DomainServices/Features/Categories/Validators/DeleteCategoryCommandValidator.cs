using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Delete;
using FluentValidation;

namespace DomainServices.Features.Categories.Validators;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCommand<Category>>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Object must be not null");

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");
    }
}