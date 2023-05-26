using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Brands.Commands.Update;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand>
{
    private readonly IRepository<Brand> _brandsRepository;

    public UpdateBrandCommandHandler(IRepository<Brand> brandsRepository)
    {
        _brandsRepository = brandsRepository;
    }

    public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        _brandsRepository.Update(request.Brand);
        await _brandsRepository.SaveChangesAsync(cancellationToken);
    }
}