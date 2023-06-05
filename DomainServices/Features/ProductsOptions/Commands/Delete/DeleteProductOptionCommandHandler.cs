using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.ProductsOptions.Commands.Delete;

public class DeleteProductOptionCommandHandler : IRequestHandler<DeleteProductOptionCommand, Unit>
{
    private readonly IRepository<ProductOption> _productOptionRepository;

    public DeleteProductOptionCommandHandler(IRepository<ProductOption> productOptionRepository)
    {
        _productOptionRepository = productOptionRepository;
    }

    public async Task<Unit> Handle(DeleteProductOptionCommand request, CancellationToken cancellationToken)
    {
        ProductOption? productOption = await _productOptionRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (productOption is not null)
        {
            _productOptionRepository.Delete(productOption);
            await _productOptionRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}