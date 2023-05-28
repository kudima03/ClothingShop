using ApplicationCore.Entities;
using ApplicationCore.Specifications.Section;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.Sections.Queries;

public class GetAllSectionsQuery : EntityCollectionQuery<Section>
{
    public GetAllSectionsQuery()
        : base(new GetSectionsWithCategories())
    {
    }
}