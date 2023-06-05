using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.ProductColors.Commands.Update;

public class UpdateProductColorCommand : IRequest<Unit>
{
    public UpdateProductColorCommand(ProductColor productColor)
    {
        ProductColor = productColor;
    }

    public ProductColor ProductColor { get; init; }
}