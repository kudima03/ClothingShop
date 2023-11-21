using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Products.Queries.GetByBrandId;

public class GetProductsByBrandIdQuery(long brandId) : IRequest<IEnumerable<Product>>
{
    public long BrandId { get; init; } = brandId;
}