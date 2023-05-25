using ApplicationCore.Entities;
using MediatR;

namespace Web.Features.Brands;

public class UpdateBrand : IRequest
{
    public UpdateBrand(Brand brand)
    {
        Brand = brand;
    }

    public Brand Brand { get; set; }
}