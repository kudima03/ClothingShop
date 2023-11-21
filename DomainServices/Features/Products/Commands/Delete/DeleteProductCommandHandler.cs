using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Products.Commands.Delete;

public class DeleteProductCommandHandler(IRepository<Product> productsRepository,
                                         IRepository<ProductColor> productColorsRepository)
    : IRequestHandler<DeleteProductCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await productsRepository.GetFirstOrDefaultAsync
                               (predicate: x => x.Id == request.Id,
                                cancellationToken: cancellationToken);

        if (product is not null)
        {
            IList<ProductColor>? productColorsToRemove = await
                                                             productColorsRepository.GetAllAsync
                                                                 (predicate: productColor =>
                                                                      productColor.ProductOptions.All
                                                                          (productOption =>
                                                                              productOption.ProductId == product.Id),
                                                                  cancellationToken: cancellationToken);

            productColorsRepository.Delete(productColorsToRemove);
            productsRepository.Delete(product);
            await productsRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}