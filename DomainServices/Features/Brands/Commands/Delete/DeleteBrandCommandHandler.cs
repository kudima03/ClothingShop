using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Brands.Commands.Delete;

public class DeleteBrandHandler : IRequestHandler<DeleteBrandCommand>
{
    private readonly IRepository<Brand> _brandsRepository;

    public DeleteBrandHandler(IRepository<Brand> brandsRepository)
    {
        _brandsRepository = brandsRepository;
    }

    public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        _brandsRepository.Delete(
            (await _brandsRepository.GetFirstOrDefaultAsync(
                predicate: brand => brand.Id == request.BrandId, cancellationToken: cancellationToken))!);
        await _brandsRepository.SaveChangesAsync(cancellationToken);
    }
}