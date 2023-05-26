using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;

namespace DomainServices.Features.Sections.Commands.Delete;

public class DeleteSectionCommandValidator : AbstractValidator<DeleteSectionCommand>
{
    public DeleteSectionCommandValidator(IReadOnlyRepository<Section> sectionsRepository)
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Object must be not null");

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");

        RuleFor(x => x.Id)
            .MustAsync(async (sectionId, cancellationToken) =>
                await sectionsRepository.ExistsAsync(x => x.Id == sectionId, cancellationToken))
            .WithMessage(x => $"Section with id:{x.Id} doesn't exist.");
    }
}