using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Products.Queries.GetBySubcategoryId;

public class
    GetProductsBySubcategoryIdQueryHandler(IReadOnlyRepository<Product> productsRepository) : IRequestHandler<GetProductsBySubcategoryIdQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetProductsBySubcategoryIdQuery request,
                                                   CancellationToken cancellationToken)
    {
        return await productsRepository.GetAllAsync
                   (predicate: x => x.SubcategoryId == request.SubcategoryId,
                    cancellationToken: cancellationToken);
    }
}