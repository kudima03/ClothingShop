using MediatR;

namespace Web.Features.Brands;

public class DeleteBrand : IRequest
{
    public DeleteBrand(long brandId)
    {
        BrandId = brandId;
    }

    public long BrandId { get; set; }
}