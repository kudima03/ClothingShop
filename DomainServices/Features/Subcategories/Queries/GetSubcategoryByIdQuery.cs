using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;

namespace DomainServices.Features.Subcategories.Queries;

public class GetSubcategoryByIdQuery : SingleEntityQuery<Subcategory>
{
    public GetSubcategoryByIdQuery(long id)
        : base(new Specification<Subcategory, Subcategory>(
            x => x,
            x => x.Id == id))
    {
        Id = id;
    }

    public long Id { get; init; }
}