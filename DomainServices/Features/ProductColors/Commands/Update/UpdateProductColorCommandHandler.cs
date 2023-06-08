using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.ProductColors.Commands.Update;

public class UpdateProductColorCommandHandler : IRequestHandler<UpdateProductColorCommand, Unit>
{
    private readonly IRepository<ProductColor> _productColorsRepository;

    public UpdateProductColorCommandHandler(IRepository<ProductColor> productColorsRepository)
    {
        _productColorsRepository = productColorsRepository;
    }

    public async Task<Unit> Handle(UpdateProductColorCommand request, CancellationToken cancellationToken)
    {
        ProductColor? productColor = await
            _productColorsRepository.GetFirstOrDefaultAsync(x => x.Id == request.ProductColor.Id,
                x => x.Include(c => c.ImagesInfos),
                cancellationToken);

        if (productColor is null)
        {
            throw new EntityNotFoundException(
                $"{nameof(ProductColor)} with id:{request.ProductColor.Id} doesn't exist.");
        }

        productColor.ImagesInfos.Clear();
        productColor.ImagesInfos.AddRange(request.ProductColor.ImagesInfos);

        try
        {
            await _productColorsRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException(
                $"Unable to perform create {nameof(ProductColor)} operation. Check input.");
        }

        return Unit.Value;
    }
}