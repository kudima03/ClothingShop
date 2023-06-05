using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Brands.Commands.Update;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Unit>
{
    private readonly IRepository<Brand> _brandsRepository;

    public UpdateBrandCommandHandler(IRepository<Brand> brandsRepository)
    {
        _brandsRepository = brandsRepository;
    }

    public async Task<Unit> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        Brand? brand = await _brandsRepository.GetFirstOrDefaultAsync(predicate: brand => brand.Id == request.Brand.Id,
            cancellationToken: cancellationToken);
        if (brand is null)
        {
            throw new EntityNotFoundException($"{nameof(Brand)} with id:{request.Brand.Id} doesn't exist.");
        }

        brand.Name = request.Brand.Name;

        await _brandsRepository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}