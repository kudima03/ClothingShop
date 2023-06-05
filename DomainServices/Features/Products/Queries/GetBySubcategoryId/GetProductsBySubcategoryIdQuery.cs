using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Products.Queries.GetBySubcategoryId;

public class GetProductsBySubcategoryIdQuery : IRequest<IEnumerable<Product>>
{
    public GetProductsBySubcategoryIdQuery(long subcategoryId)
    {
        SubcategoryId = subcategoryId;
    }

    public long SubcategoryId { get; init; }
}