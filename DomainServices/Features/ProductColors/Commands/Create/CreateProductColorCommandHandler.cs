using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.ProductColors.Commands.Create;

public class CreateProductColorCommandHandler : IRequestHandler<CreateProductColorCommand, ProductColor>
{
    private readonly IRepository<ProductColor> _productColorsRepository;

    public CreateProductColorCommandHandler(IRepository<ProductColor> productColorsRepository)
    {
        _productColorsRepository = productColorsRepository;
    }

    public async Task<ProductColor> Handle(CreateProductColorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ProductColor? productColor =
                await _productColorsRepository.InsertAsync(request.ProductColor, cancellationToken);
            await _productColorsRepository.SaveChangesAsync(cancellationToken);
            return productColor;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException(
                $"Unable to perform create {nameof(ProductColor)} operation. Check input.");
        }
    }
}