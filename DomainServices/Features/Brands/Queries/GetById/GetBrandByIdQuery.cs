using ApplicationCore.Entities;
using ApplicationCore.Specifications.Brand;
using MediatR;

namespace DomainServices.Features.Brands.Queries.GetById;

public class GetBrandByIdQuery : IRequest<Brand?>
{
    public GetBrandByIdQuery(long id)
    {
        Id = id;
        Specification = new GetBrandWithProducts(brand => brand.Id == id);
    }

    public long Id { get; init; }

    public GetBrandWithProducts Specification { get; init; }
}