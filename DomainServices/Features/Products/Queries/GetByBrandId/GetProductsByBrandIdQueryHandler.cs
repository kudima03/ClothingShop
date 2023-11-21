using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Products.Queries.GetByBrandId;

public class GetProductsByBrandIdQueryHandler(IReadOnlyRepository<Product> repository) : IRequestHandler<GetProductsByBrandIdQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetProductsByBrandIdQuery request,
                                                   CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync
                   (predicate: x => x.BrandId == request.BrandId,
                    cancellationToken: cancellationToken);
    }
}