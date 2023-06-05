using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.ProductColors.Commands.Create;

public class CreateProductColorCommand : IRequest<ProductColor>
{
    public CreateProductColorCommand(ProductColor productColor)
    {
        ProductColor = productColor;
    }

    public ProductColor ProductColor { get; init; }
}