using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Products.Commands.Update;

public class UpdateProductCommand : IRequest<Unit>
{
    public long Id { get; init; }
    public long BrandId { get; init; }
    public long SubcategoryId { get; init; }
    public string Name { get; init; }
    public virtual ICollection<ProductOption> ProductOptionsDto { get; init; } = new List<ProductOption>();
}