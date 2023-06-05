using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Products.Commands.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IRepository<Product> _productsRepository;

    public DeleteProductCommandHandler(IRepository<Product> productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productsRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (product is not null)
        {
            _productsRepository.Delete(product);
            await _productsRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}