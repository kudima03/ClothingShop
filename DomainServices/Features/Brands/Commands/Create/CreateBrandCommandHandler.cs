using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DomainServices.Features.Brands.Commands.Create;

public class CreateBrandCommandHandler(IRepository<Brand> brandsRepository) : IRequestHandler<CreateBrandCommand, Brand>
{
    public async Task<Brand> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        await ValidateBrandNameAsync(request.Name, cancellationToken);

        Brand newBrand = new Brand
        {
            Name = request.Name
        };

        Brand? insertedBrand = await brandsRepository.InsertAsync(newBrand, cancellationToken);
        await brandsRepository.SaveChangesAsync(cancellationToken);

        return insertedBrand;
    }

    private async Task ValidateBrandNameAsync(string name, CancellationToken cancellationToken = default)
    {
        bool nameExists = await brandsRepository.ExistsAsync(x => x.Name == name, cancellationToken);

        if (nameExists)
        {
            throw new ValidationException
                (new[]
                {
                    new ValidationFailure("Brand.Name", "Such brand already exists!")
                });
        }
    }
}