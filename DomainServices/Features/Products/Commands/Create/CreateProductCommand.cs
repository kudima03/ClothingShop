using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Products.Commands.Create;

public class CreateProductCommand : IRequest<Product>
{
    public long Id { get; init; }

    public long BrandId { get; init; }

    public long SubcategoryId { get; init; }

    public string Name { get; init; }

    public ICollection<CreateUpdateProductCommandsDtos.ProductOptionDto> ProductOptionsDtos { get; init; }
        = new List<CreateUpdateProductCommandsDtos.ProductOptionDto>();
}