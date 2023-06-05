using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

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
        return await _sectionsRepository.GetAllAsync(cancellationToken);
    }
}