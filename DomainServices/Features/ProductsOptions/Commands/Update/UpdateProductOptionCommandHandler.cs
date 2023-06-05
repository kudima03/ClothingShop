using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.ProductsOptions.Commands.Update;

public class UpdateProductOptionCommandHandler : IRequestHandler<UpdateProductOptionCommand, Unit>
{
    private readonly IRepository<ProductOption> _repository;

    public UpdateProductOptionCommandHandler(IRepository<ProductOption> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateProductOptionCommand request, CancellationToken cancellationToken)
    {
        ProductOption? productOption = await _repository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == request.ProductOption.Id,
            cancellationToken: cancellationToken);

        if (productOption is null)
        {
            throw new EntityNotFoundException(
                $"{nameof(ProductOption)} with id:{request.ProductOption.Id} doesn't exist.");
        }

        productOption.ProductId = request.ProductOption.ProductId;
        productOption.ProductColorId = request.ProductOption.ProductColorId;
        productOption.Size = request.ProductOption.Size;
        productOption.Quantity = request.ProductOption.Quantity;
        productOption.Price = request.ProductOption.Price;

        try
        {
            await _repository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException(
                $"Unable to perform update {nameof(ProductOption)} operation. Check input.");
        }

        return Unit.Value;
    }
}