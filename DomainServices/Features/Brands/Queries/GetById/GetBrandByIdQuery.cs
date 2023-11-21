using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Brands.Queries.GetById;

public class GetBrandByIdQuery(long id) : IRequest<Brand>
{
    public long Id { get; init; } = id;
}