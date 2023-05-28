using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.Products.Queries;

public class GetProductsBySubcategoryIdQuery : EntityCollectionQuery<Product>
{
    public GetProductsBySubcategoryIdQuery(long subcategoryId)
        : base(new Specification<Product, Product>(product => product,
            product => product.SubcategoryId == subcategoryId))

    {
        SubcategoryId = subcategoryId;
    }

    public long SubcategoryId { get; init; }
}