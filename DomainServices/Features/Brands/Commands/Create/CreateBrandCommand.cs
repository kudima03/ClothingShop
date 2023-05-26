using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<Brand>
{
    public CreateBrandCommand(Brand brand)
    {
        Brand = brand;
    }

    public Brand Brand { get; init; }
}