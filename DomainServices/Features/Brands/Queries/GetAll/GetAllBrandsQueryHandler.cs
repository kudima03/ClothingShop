using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Brands.Queries.GetAll;

public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<Brand>>
{
    private readonly IReadOnlyRepository<Brand> _brandsRepository;

    public GetAllBrandsQueryHandler(IReadOnlyRepository<Brand> brandsRepository)
    {
        _brandsRepository = brandsRepository;
    }

    public async Task<IEnumerable<Brand>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        return await _brandsRepository.GetAllAsync(cancellationToken);
    }
}