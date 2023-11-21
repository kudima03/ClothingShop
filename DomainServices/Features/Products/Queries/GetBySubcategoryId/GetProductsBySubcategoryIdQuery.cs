using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Products.Queries.GetBySubcategoryId;

public class GetProductsBySubcategoryIdQuery(long subcategoryId) : IRequest<IEnumerable<Product>>
{
    public long SubcategoryId { get; init; } = subcategoryId;
}