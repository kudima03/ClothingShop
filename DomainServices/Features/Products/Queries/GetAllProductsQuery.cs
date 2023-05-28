using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.Products.Queries;

public class GetAllProductsQuery : EntityCollectionQuery<Product>
{
    public GetAllProductsQuery()
        : base(new Specification<Product, Product>(x => x))
    {
    }
}