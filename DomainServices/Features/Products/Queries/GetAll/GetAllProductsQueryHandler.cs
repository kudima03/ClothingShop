using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Products.Queries.GetAll;

public class GetAllProductsQueryHandler(IReadOnlyRepository<Product> productsRepository) : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await productsRepository.ApplySpecification(new ProductWithBrandSubcategoryReviewsOptionsColorsImages())
                                        .ToListAsync(cancellationToken);
    }
}