using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Products.Queries.GetById;

public class GetProductByIdQuery : IRequest<Product?>
{
    public GetProductByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}