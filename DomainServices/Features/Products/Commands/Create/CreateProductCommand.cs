using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Products.Commands.Create;

public class CreateProductCommand : IRequest<Product>
{
    public long BrandId { get; init; }
    public long SubcategoryId { get; init; }
    public string Name { get; init; }
    public virtual List<ProductOption> ProductOptions { get; init; } = new();
}