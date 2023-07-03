using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Products.Queries.GetAll;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    private readonly IReadOnlyRepository<Product> _productsRepository;

    public GetAllProductsQueryHandler(IReadOnlyRepository<Product> productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productsRepository.ApplySpecification(new ProductWithBrandSubcategoryReviewsOptionsColorsImages())
                                        .ToListAsync(cancellationToken);
    }
}