using ApplicationCore.Entities;
using MediatR;

namespace Web.Features.Brands;

public class GetBrandById : IRequest<Brand>
{
    public GetBrandById(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}