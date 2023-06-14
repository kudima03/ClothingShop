using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Products.Commands.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IRepository<ProductColor> _productColorsRepository;
    private readonly IRepository<Product> _productsRepository;

    public DeleteProductCommandHandler(IRepository<Product> productsRepository,
                                       IRepository<ProductColor> productColorsRepository)
    {
        _productsRepository = productsRepository;
        _productColorsRepository = productColorsRepository;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productsRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
                                                                            cancellationToken: cancellationToken);

        if (product is not null)
        {
            IList<ProductColor>? productColorsToRemove = await
                                                             _productColorsRepository
                                                                 .GetAllAsync(predicate: productColor =>
                                                                                  productColor.ProductOptions
                                                                                      .All(productOption =>
                                                                                          productOption
                                                                                              .ProductId ==
                                                                                          product.Id),
                                                                              cancellationToken: cancellationToken);

            _productColorsRepository.Delete(productColorsToRemove);
            _productsRepository.Delete(product);
            await _productsRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}