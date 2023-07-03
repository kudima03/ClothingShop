using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Products.Queries.GetByBrandId;

public class GetProductsByBrandIdQueryHandler : IRequestHandler<GetProductsByBrandIdQuery, IEnumerable<Product>>
{
    private readonly IReadOnlyRepository<Product> _repository;

    public GetProductsByBrandIdQueryHandler(IReadOnlyRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsByBrandIdQuery request,
                                                   CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync
                   (predicate: x => x.BrandId == request.BrandId,
                    cancellationToken: cancellationToken);
    }
}