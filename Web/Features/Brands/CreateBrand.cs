using ApplicationCore.Entities;
using MediatR;

namespace Web.Features.Brands;

public class CreateBrand : IRequest<Brand>
{
    public CreateBrand(Brand brand)
    {
        Brand = brand;
    }

    public Brand Brand { get; set; }
}