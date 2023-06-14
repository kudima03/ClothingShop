using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Section;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Sections.Queries.GetById;

public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, Section>
{
    private readonly IReadOnlyRepository<Section> _sectionsRepository;

    public GetSectionByIdQueryHandler(IReadOnlyRepository<Section> sectionsRepository)
    {
        _sectionsRepository = sectionsRepository;
    }

    public async Task<Section> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
    {
        Section? section = await _sectionsRepository
                                 .ApplySpecification(new SectionWithCategories(x => x.Id == request.Id))
                                 .FirstOrDefaultAsync(cancellationToken);

        if (section is null)
        {
            throw new EntityNotFoundException($"{nameof(Section)} with id:{request.Id} doesn't exist.");
        }

        return section;
    }
}