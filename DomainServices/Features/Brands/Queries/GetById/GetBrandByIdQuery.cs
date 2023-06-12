using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Brands.Queries.GetById;

public class GetBrandByIdQuery : IRequest<Brand>
{
    public GetBrandByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}