using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Products.Commands.Update;

public class UpdateProductCommand : IRequest<Unit>
{
    public UpdateProductCommand(Product product)
    {
        Product = product;
    }

    public Product Product { get; init; }
}