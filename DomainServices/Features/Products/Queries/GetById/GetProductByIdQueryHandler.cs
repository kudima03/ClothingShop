using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Products.Queries.GetById;

public class GetProductByIdQueryHandler(IReadOnlyRepository<Product> repository) : IRequestHandler<GetProductByIdQuery, Product>
{
    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product? product = await repository
                                 .ApplySpecification
                                     (new ProductWithBrandSubcategoryReviewsOptionsColorsImages(x => x.Id == request.Id))
                                 .SingleOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            throw new EntityNotFoundException($"{nameof(Product)} with id:{request.Id} doesn't exist.");
        }

        return product;
    }
}