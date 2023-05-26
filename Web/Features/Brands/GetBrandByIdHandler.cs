using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace Web.Features.Brands;

public class GetBrandByIdHandler : IRequestHandler<GetBrandById, Brand?>
{
    private readonly IReadOnlyRepository<Brand> _brandsRepository;

    public GetBrandByIdHandler(IReadOnlyRepository<Brand> brandsRepository)
    {
        _brandsRepository = brandsRepository;
    }

    public async Task<Brand?> Handle(GetBrandById request, CancellationToken cancellationToken)
    {
        return await _brandsRepository.GetFirstOrDefaultNonTrackingAsync(predicate: brand => brand.Id == request.Id, cancellationToken: cancellationToken);
    }
}