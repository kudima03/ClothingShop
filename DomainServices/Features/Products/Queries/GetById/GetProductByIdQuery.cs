using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Products.Queries.GetById;

public class GetProductByIdQuery(long id) : IRequest<Product>
{
    public long Id { get; init; } = id;
}