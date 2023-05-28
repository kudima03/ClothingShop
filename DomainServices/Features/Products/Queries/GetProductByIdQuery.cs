using ApplicationCore.Entities;
using ApplicationCore.Specifications.Product;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;

namespace DomainServices.Features.Products.Queries;

public class GetProductByIdQuery : SingleEntityQuery<Product>
{
    public GetProductByIdQuery(long id)
        : base(new ProductWithBrandSubcategoryOptionsReviews(x => x.Id == id))
    {
        Id = id;
    }

    public long Id { get; init; }
}