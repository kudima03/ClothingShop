using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Brand;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Brands.Queries.GetById;

public class GetBrandByIdQueryHandler(IReadOnlyRepository<Brand> brandsRepository) : IRequestHandler<GetBrandByIdQuery, Brand>
{
    public async Task<Brand> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        Brand? brand = await brandsRepository
                             .ApplySpecification(new GetBrandWithProducts(brand => brand.Id == request.Id))
                             .FirstOrDefaultAsync(cancellationToken);

        if (brand is null)
        {
            throw new EntityNotFoundException($"{nameof(Brand)} with id:{request.Id} doesn't exist.");
        }

        return brand;
    }
}