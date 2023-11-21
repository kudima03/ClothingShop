using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Brands.Commands.Delete;

public class DeleteBrandCommandHandler(IRepository<Brand> brandsRepository) : IRequestHandler<DeleteBrandCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        Brand? brand = await brandsRepository.GetFirstOrDefaultAsync
                           (predicate: x => x.Id == request.Id,
                            cancellationToken: cancellationToken);

        if (brand is not null)
        {
            brandsRepository.Delete(brand);
            await brandsRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}