using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Section;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Sections.Queries.GetById;

public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, Section?>
{
    private readonly IReadOnlyRepository<Section> _sectionsRepository;

    public GetSectionByIdQueryHandler(IReadOnlyRepository<Section> sectionsRepository)
    {
        _sectionsRepository = sectionsRepository;
    }

    public async Task<Section?> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
    {
        return await _sectionsRepository.ApplySpecification(new GetSectionByIdWithCategories(request.Id))
            .FirstOrDefaultAsync(cancellationToken);
    }
}