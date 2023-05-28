using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;

namespace DomainServices.Features.Colors.Queries;

public class GetColorByIdQuery : SingleEntityQuery<Color>
{
    public GetColorByIdQuery(long id)
        : base(new Specification<Color, Color>(x => x,
            x => x.Id == id))
    {
        Id = id;
    }

    public long Id { get; init; }
}