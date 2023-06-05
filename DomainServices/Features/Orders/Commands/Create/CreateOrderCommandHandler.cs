using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Orders.Commands.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly IRepository<Order> _ordersRepository;

    private readonly IRepository<ProductOption> _productOptionsRepository;

    public CreateOrderCommandHandler(IRepository<Order> ordersRepository,
        IRepository<ProductOption> productOptionsRepository)
    {
        _ordersRepository = ordersRepository;
        _productOptionsRepository = productOptionsRepository;
    }

    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await ValidateAsync(request.Order);
        try
        {
            Order? order = await _ordersRepository.InsertAsync(request.Order, cancellationToken);
            await _ordersRepository.SaveChangesAsync(cancellationToken);
            return order;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Order)} operation. Check input.");
        }
    }

    private async Task ValidateAsync(Order newOrder)
    {
        //TODO: Check is it works
        bool exists = await _productOptionsRepository.ExistsAsync(productOption =>
            newOrder.ProductsOptions.Select(option => option.Id).Contains(productOption.Id));

        if (!exists)
        {
            throw new ValidationException("Validation error",
                new[]
                {
                    new ValidationFailure(nameof(newOrder.ProductsOptions),
                        $"One of {nameof(newOrder.ProductsOptions)} doesn't exist.")
                });
        }
    }
}