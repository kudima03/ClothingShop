using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Brands.Commands.Delete;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Unit>
{
    private readonly IRepository<Brand> _brandsRepository;

    public DeleteBrandCommandHandler(IRepository<Brand> brandsRepository)
    {
        _brandsRepository = brandsRepository;
    }

    public async Task<Unit> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        Brand? brand = await _brandsRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (brand is not null)
        {
            _brandsRepository.Delete(brand);
            await _brandsRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}