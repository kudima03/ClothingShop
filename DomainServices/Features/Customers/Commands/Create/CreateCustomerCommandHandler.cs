using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Customers.Commands.Create;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerInfo>
{
    private readonly IRepository<CustomerInfo> _customersInfoRepository;

    public CreateCustomerCommandHandler(IRepository<CustomerInfo> customersInfoRepository)
    {
        _customersInfoRepository = customersInfoRepository;
    }

    public async Task<CustomerInfo> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            CustomerInfo? customer =
                await _customersInfoRepository.InsertAsync(request.CustomerInfo, cancellationToken);
            await _customersInfoRepository.SaveChangesAsync(cancellationToken);
            return customer;
        }
        catch (Exception)
        {
            throw new OperationFailureException(
                $"Unable to perform create {nameof(CustomerInfo)} operation. Check input.");
        }
    }
}