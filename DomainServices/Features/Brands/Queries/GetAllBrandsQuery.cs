using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.Brands.Queries;

public class GetAllBrandsQuery : EntityCollectionQuery<Brand>
{
    public GetAllBrandsQuery() :
        base(new Specification<Brand, Brand>(x => x))
    {
    }
}