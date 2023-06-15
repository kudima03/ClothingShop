using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
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
        Brand brand = await GetBrandToUpdateAsync(request.Id, cancellationToken);

        if (brand.Name == request.Name)
        {
            return Unit.Value;
        }

        await ValidateBrandNameAsync(request.Name, cancellationToken);

        brand.Name = request.Name;

        await _brandsRepository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    private async Task<Brand> GetBrandToUpdateAsync(long id, CancellationToken cancellationToken = default)
    {
        Brand? brand = await _brandsRepository.GetFirstOrDefaultAsync(predicate: brand => brand.Id == id,
            cancellationToken: cancellationToken);
        if (brand is null)
        {
            throw new EntityNotFoundException($"{nameof(Brand)} with id:{id} doesn't exist.");
        }

        return brand;
    }

    private async Task ValidateBrandNameAsync(string name, CancellationToken cancellationToken = default)
    {
        bool nameExists = await _brandsRepository.ExistsAsync(x => x.Name == name, cancellationToken);
        if (nameExists)
        {
            throw new ValidationException(new[] { new ValidationFailure("Brand.Name", "Such brand already exists!") });
        }
    }
}