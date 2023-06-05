using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.ProductsOptions.Commands.Update;

public class UpdateProductOptionCommand : IRequest<Unit>
{
    public UpdateProductOptionCommand(ProductOption productOption)
    {
        ProductOption = productOption;
    }

    public ProductOption ProductOption { get; init; }
}