using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace Web.Features.Brands;

public class DeleteBrandHandler : IRequestHandler<DeleteBrand>
{
    private readonly IRepository<Brand> _brandsRepository;

    private readonly IReadOnlyRepository<Brand> _readOnlyBrandsRepository;

    public DeleteBrandHandler(IRepository<Brand> brandsRepository, IReadOnlyRepository<Brand> readOnlyBrandsRepository)
    {
        _brandsRepository = brandsRepository;
        _readOnlyBrandsRepository = readOnlyBrandsRepository;
    }

    public async Task Handle(DeleteBrand request, CancellationToken cancellationToken)
    {
        Brand? brandToDelete =
            await _readOnlyBrandsRepository.GetFirstOrDefaultNonTrackingAsync(predicate: brand =>
                brand.Id == request.BrandId);

        if (brandToDelete is null)
        {
            return;
        }

        _brandsRepository.Delete(brandToDelete);
        await _brandsRepository.SaveChangesAsync();
    }
}