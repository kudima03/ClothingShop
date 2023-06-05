using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

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
            await _customersInfoRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.CustomerInfo.Id,
                cancellationToken: cancellationToken);

        if (customerInfo is null)
        {
            throw new EntityNotFoundException(
                $"{nameof(CustomerInfo)} with id:{request.CustomerInfo.Id} doesn't exist.");
        }

        customerInfo.Name = request.CustomerInfo.Name;
        customerInfo.Surname = request.CustomerInfo.Surname;
        customerInfo.Patronymic = request.CustomerInfo.Patronymic;
        customerInfo.Address = request.CustomerInfo.Address;
        customerInfo.Phone = request.CustomerInfo.Phone;

        await _customersInfoRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}