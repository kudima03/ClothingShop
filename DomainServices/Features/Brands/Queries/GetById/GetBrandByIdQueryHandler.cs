using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

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
        return await _brandsRepository.GetFirstOrDefaultNonTrackingAsync(predicate: brand => brand.Id == request.Id,
            cancellationToken: cancellationToken);
    }
}