using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;

namespace DomainServices.Features.Sections.Commands.Update;

public class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
{
    public UpdateSectionCommandValidator(IReadOnlyRepository<Section> sectionsRepository)
    {
        RuleFor(x => x.Section)
            .NotNull()
            .WithMessage("Object cannot be null");
        RuleFor(x => x.Section.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name must not be null or empty");

        RuleFor(x => x.Section.Id)
            .MustAsync(async (sectionId, cancellationToken) =>
                await sectionsRepository.ExistsAsync(x => x.Id == sectionId, cancellationToken))
            .WithMessage(x => $"Section with id:{x.Section.Id} doesn't exist.");
    }
}