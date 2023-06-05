using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Brands.Commands.Create;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Brand>
{
    private readonly IRepository<Brand> _brandsRepository;

    public CreateBrandCommandHandler(IRepository<Brand> brandsRepository)
    {
        _brandsRepository = brandsRepository;
    }

    public async Task<Brand> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        Brand? brand = await _brandsRepository.InsertAsync(request.Brand, cancellationToken);
        await _brandsRepository.SaveChangesAsync(cancellationToken);
        return brand;
    }
}