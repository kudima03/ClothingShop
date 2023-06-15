using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
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
        await ValidateBrandNameAsync(request.Name, cancellationToken);
        Brand newBrand = new()
        {
            Name = request.Name
        };
        Brand? insertedBrand = await _brandsRepository.InsertAsync(newBrand, cancellationToken);
        await _brandsRepository.SaveChangesAsync(cancellationToken);
        return insertedBrand;
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