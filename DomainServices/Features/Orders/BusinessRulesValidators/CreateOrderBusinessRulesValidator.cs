using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Features.Templates.BusinessRulesValidators;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;
using FluentValidation.Results;

namespace DomainServices.Features.Orders.BusinessRulesValidators;

public class CreateOrderBusinessRulesValidator : IBusinessRulesValidator<CreateCommand<Order>>
{
    private readonly IRepository<OrderStatus> _orderStatusesRepository;

    private readonly IRepository<User> _usersRepository;

    public CreateOrderBusinessRulesValidator(IRepository<OrderStatus> orderStatusesRepository,
        IRepository<User> usersRepository)
    {
        _orderStatusesRepository = orderStatusesRepository;
        _usersRepository = usersRepository;
    }

    public async Task ValidateAsync(CreateCommand<Order> entity, CancellationToken cancellation = default)
    {
        if (!await _orderStatusesRepository.ExistsAsync(x => x.Id == entity.Entity.OrderStatusId, cancellation))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(entity.Entity.OrderStatusId)}",
                    $"{entity.Entity.OrderStatus.GetType().Name} with id: {entity.Entity.OrderStatusId} doesn't exist")
            });
        }

        if (!await _usersRepository.ExistsAsync(x => x.Id == entity.Entity.UserId, cancellation))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(entity.Entity.UserId)}",
                    $"{entity.Entity.User.GetType().Name} with id: {entity.Entity.UserId} doesn't exist")
            });
        }
    }
}