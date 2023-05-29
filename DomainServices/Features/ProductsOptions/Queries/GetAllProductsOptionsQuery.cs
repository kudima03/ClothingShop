using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.ProductsOptions.Queries;

public class GetAllProductsOptionsQuery : EntityCollectionQuery<ProductOption>
{
    public GetAllProductsOptionsQuery()
        : base(new Specification<ProductOption, ProductOption>(x => x))
    {
    }
}