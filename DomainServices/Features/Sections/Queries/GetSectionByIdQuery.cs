using ApplicationCore.Entities;
using ApplicationCore.Specifications.Section;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;

namespace DomainServices.Features.Sections.Queries;

public class GetSectionByIdQuery : SingleEntityQuery<Section>
{
    public GetSectionByIdQuery(long id)
        : base(new GetSectionByIdWithCategories(id))
    {
        Id = id;
    }

    public long Id { get; init; }
}