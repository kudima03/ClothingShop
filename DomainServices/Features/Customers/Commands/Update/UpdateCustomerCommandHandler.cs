using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Customers.Commands.Update;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{
    private readonly IRepository<CustomerInfo> _customersInfoRepository;

    public UpdateCustomerCommandHandler(IRepository<CustomerInfo> customersInfoRepository)
    {
        _customersInfoRepository = customersInfoRepository;
    }

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        CustomerInfo? customerInfo =
            await _customersInfoRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
                cancellationToken: cancellationToken);

        if (customerInfo is null)
        {
            throw new EntityNotFoundException(
                $"{nameof(CustomerInfo)} with id:{request.Id} doesn't exist.");
        }

        customerInfo.Name = request.Name;
        customerInfo.Surname = request.Surname;
        customerInfo.Patronymic = request.Patronymic;
        customerInfo.Address = request.Address;
        customerInfo.Phone = request.Phone;

        try
        {
            await _customersInfoRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException(
                $"Unable to perform create {nameof(CustomerInfo)} operation. Check input.");
        }

        return Unit.Value;
    }
}