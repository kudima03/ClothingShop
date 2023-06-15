using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Products.Queries.GetByBrandId;

public class GetProductsByBrandIdQuery : IRequest<IEnumerable<Product>>
{
    public GetProductsByBrandIdQuery(long brandId)
    {
        BrandId = brandId;
    }

    public long BrandId { get; init; }
}