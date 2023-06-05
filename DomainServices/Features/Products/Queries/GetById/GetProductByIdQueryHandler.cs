using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Products.Queries.GetById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IReadOnlyRepository<Product> _repository;

    public GetProductByIdQueryHandler(IReadOnlyRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository
            .ApplySpecification(new ProductWithBrandSubcategoryOptionsReviews(x => x.Id == request.Id))
            .SingleOrDefaultAsync(cancellationToken);
    }
}