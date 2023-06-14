using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Products.Queries.GetBySubcategoryId;

public class
    GetProductsBySubcategoryIdQueryHandler : IRequestHandler<GetProductsBySubcategoryIdQuery, IEnumerable<Product>>
{
    private readonly IReadOnlyRepository<Product> _productsRepository;

    public GetProductsBySubcategoryIdQueryHandler(IReadOnlyRepository<Product> productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsBySubcategoryIdQuery request,
                                                   CancellationToken cancellationToken)
    {
        return await _productsRepository.GetAllAsync(predicate: x => x.SubcategoryId == request.SubcategoryId,
                                                     cancellationToken: cancellationToken);
    }
}