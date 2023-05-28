using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.Products.Queries;

public class GetProductsByBrandIdQuery : EntityCollectionQuery<Product>
{
    public GetProductsByBrandIdQuery(long brandId)
        : base(new Specification<Product, Product>(product => product,
            product => product.BrandId == brandId))
    {
        BrandId = brandId;
    }

    public long BrandId { get; set; }
}