using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Products.Commands.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IRepository<Product> _productsRepository;

    public CreateProductCommandHandler(IRepository<Product> productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Product? product = await _productsRepository.InsertAsync(request.Product, cancellationToken);
            await _productsRepository.SaveChangesAsync(cancellationToken);
            return product;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Product)} operation. Check input.");
        }
    }
}