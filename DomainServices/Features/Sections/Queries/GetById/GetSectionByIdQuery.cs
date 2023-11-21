using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Sections.Queries.GetById;

public class GetSectionByIdQuery(long id) : IRequest<Section>
{
    public long Id { get; init; } = id;
}