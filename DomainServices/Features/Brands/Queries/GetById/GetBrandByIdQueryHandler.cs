using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Brands.Queries.GetById;

public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Brand?>
{
    private readonly IReadOnlyRepository<Brand> _brandsRepository;

    public GetBrandByIdQueryHandler(IReadOnlyRepository<Brand> brandsRepository)
    {
        _brandsRepository = brandsRepository;
    }

    public async Task<Brand?> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        return await _brandsRepository.ApplySpecification(request.Specification).FirstOrDefaultAsync(cancellationToken);
    }
}