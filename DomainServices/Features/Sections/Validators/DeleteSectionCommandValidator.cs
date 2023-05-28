using ApplicationCore.Entities;
using DomainServices.Features.Templates.Commands.Delete;
using FluentValidation;

namespace DomainServices.Features.Sections.Validators;

public class DeleteSectionCommandValidator : AbstractValidator<DeleteCommand<Section>>
{
    public DeleteSectionCommandValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Object must be not null");

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");
    }
}