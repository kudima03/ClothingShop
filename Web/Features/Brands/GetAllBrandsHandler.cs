using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace Web.Features.Brands;

public class GetAllBrandsHandler : IRequestHandler<GetAllBrands, IEnumerable<Brand>>
{
    private readonly IReadOnlyRepository<Brand> _brandsRepository;

    public GetAllBrandsHandler(IReadOnlyRepository<Brand> brandsRepository)
    {
        _brandsRepository = brandsRepository;
    }

    public async Task<IEnumerable<Brand>> Handle(GetAllBrands request, CancellationToken cancellationToken)
    {
        return await _brandsRepository.GetAllNonTrackingAsync();
    }
}