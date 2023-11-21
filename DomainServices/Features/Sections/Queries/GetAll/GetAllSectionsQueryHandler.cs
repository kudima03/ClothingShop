using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Sections.Queries.GetAll;

public class GetAllSectionsQueryHandler(IReadOnlyRepository<Section> sectionsRepository) : IRequestHandler<GetAllSectionsQuery, IEnumerable<Section>>
{
    public async Task<IEnumerable<Section>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
    {
        return await sectionsRepository.GetAllAsync(cancellationToken);
    }
}