using ApplicationCore.Entities;
using ApplicationCore.Specifications.ProductOption;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;

namespace DomainServices.Features.ProductsOptions.Queries;

public class GetProductOptionByIdQuery : SingleEntityQuery<ProductOption>
{
    public GetProductOptionByIdQuery(long id)
        : base(new ProductOptionWithColorAndImages(x => x.Id == id))
    {
        Id = id;
    }

    public long Id { get; init; }
}