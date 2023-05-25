using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace Web.Features.Brands;

public class UpdateBrandHandler : IRequestHandler<UpdateBrand>
{
    private readonly IRepository<Brand> _brandsRepository;

    public UpdateBrandHandler(IRepository<Brand> brandsRepository)
    {
        _brandsRepository = brandsRepository;
    }

    public async Task Handle(UpdateBrand request, CancellationToken cancellationToken)
    {
        _brandsRepository.Update(request.Brand);
        await _brandsRepository.SaveChangesAsync();
    }
}