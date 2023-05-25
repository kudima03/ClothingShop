using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace Web.Features.Brands;

public class CreateBrandHandler : IRequestHandler<CreateBrand, Brand>
{
    private readonly IRepository<Brand> _brandsRepository;

    public CreateBrandHandler(IRepository<Brand> brandsRepository)
    {
        _brandsRepository = brandsRepository;
    }

    public async Task<Brand> Handle(CreateBrand request, CancellationToken cancellationToken)
    {
        Brand? entity = await _brandsRepository.InsertAsync(request.Brand, cancellationToken);
        await _brandsRepository.SaveChangesAsync();
        return entity;
    }
}