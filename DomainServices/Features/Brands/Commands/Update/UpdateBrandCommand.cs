using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Brands.Commands.Update;

public class UpdateBrandCommand : IRequest
{
    public UpdateBrandCommand(Brand brand)
    {
        Brand = brand;
    }

    public Brand Brand { get; init; }
}