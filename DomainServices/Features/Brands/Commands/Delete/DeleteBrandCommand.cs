using MediatR;

namespace DomainServices.Features.Brands.Commands.Delete;

public class DeleteBrandCommand : IRequest
{
    public DeleteBrandCommand(long brandId)
    {
        BrandId = brandId;
    }

    public long BrandId { get; init; }
}