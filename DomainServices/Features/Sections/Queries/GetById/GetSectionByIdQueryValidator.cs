using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;

namespace DomainServices.Features.Sections.Queries.GetById;

public class GetSectionByIdQueryValidator : AbstractValidator<GetSectionByIdQuery>
{
    public GetSectionByIdQueryValidator(IReadOnlyRepository<Section> sectionsRepository)
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