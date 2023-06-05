using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Products.Commands.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IRepository<Product> _productRepository;

    public UpdateProductCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await
            _productRepository.GetFirstOrDefaultAsync(x => x.Id == request.Product.Id,
                x => x.Include(c => c.ProductOptions),
                cancellationToken);

        if (product is null)
        {
            throw new EntityNotFoundException($"{nameof(Product)} with id:{request.Product.Id} doesn't exist.");
        }

        product.Name = request.Product.Name;
        product.SubcategoryId = request.Product.SubcategoryId;
        product.BrandId = request.Product.BrandId;
        product.ProductOptions.AddRange(request.Product.ProductOptions);

        try
        {
            await _productRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform update {nameof(Product)} operation. Check input.");
        }

        return Unit.Value;
    }
}