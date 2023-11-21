using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Brands.Queries.GetAll;

public class GetAllBrandsQueryHandler(IReadOnlyRepository<Brand> brandsRepository) : IRequestHandler<GetAllBrandsQuery, IEnumerable<Brand>>
{
    public async Task<IEnumerable<Brand>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        return await brandsRepository.GetAllAsync(cancellationToken);
    }
}