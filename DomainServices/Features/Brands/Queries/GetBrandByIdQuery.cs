using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;

namespace DomainServices.Features.Brands.Queries;

public class GetBrandByIdQuery : SingleEntityQuery<Brand>
{
    public GetBrandByIdQuery(long id)
        : base(new Specification<Brand, Brand>(x => x, x => x.Id == id))
    {
        Id = id;
    }

    public long Id { get; init; }
}