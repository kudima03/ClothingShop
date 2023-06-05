using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.ProductColors.Commands.Delete;

public class DeleteProductColorCommandHandler : IRequestHandler<DeleteProductColorCommand, Unit>
{
    private readonly IRepository<ProductColor> _productColorsRepository;

    public DeleteProductColorCommandHandler(IRepository<ProductColor> productColorsRepository)
    {
        _productColorsRepository = productColorsRepository;
    }

    public async Task<Unit> Handle(DeleteProductColorCommand request, CancellationToken cancellationToken)
    {
        ProductColor? productColor = await _productColorsRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (productColor is not null)
        {
            _productColorsRepository.Delete(productColor);
            await _productColorsRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}