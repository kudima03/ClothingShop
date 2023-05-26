using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Sections.Queries.GetById;

public class GetSectionByIdQuery : IRequest<Section>
{
    public GetSectionByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}