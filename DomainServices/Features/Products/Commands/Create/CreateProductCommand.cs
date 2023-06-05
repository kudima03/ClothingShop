using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Products.Commands.Create;

public class CreateProductCommand : IRequest<Product>
{
    public CreateProductCommand(Product product)
    {
        Product = product;
    }

    public Product Product { get; init; }
}