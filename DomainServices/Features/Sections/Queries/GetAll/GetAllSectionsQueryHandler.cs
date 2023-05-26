using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Section;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Sections.Queries.GetAll;

public class GetAllSectionsQueryHandler : IRequestHandler<GetAllSectionsQuery, IEnumerable<Section>>
{
    private readonly IReadOnlyRepository<Section> _sectionsRepository;

    public GetAllSectionsQueryHandler(IReadOnlyRepository<Section> sectionsRepository)
    {
        _sectionsRepository = sectionsRepository;
    }

    public async Task<IEnumerable<Section>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
    {
        return await _sectionsRepository.ApplySpecification(new GetSectionsWithCategories())
            .ToListAsync(cancellationToken);
        ;
    }
}