using ApplicationCore.Entities;
using ApplicationCore.Specifications.Section;
using MediatR;

namespace DomainServices.Features.Sections.Queries.GetById;

public class GetSectionByIdQuery : IRequest<Section?>
{
    public GetSectionByIdQuery(long id)
    {
        Id = id;
        Specification = new SectionWithCategories(x => x.Id == id);
    }

    public long Id { get; init; }
    public SectionWithCategories Specification { get; init; }
}