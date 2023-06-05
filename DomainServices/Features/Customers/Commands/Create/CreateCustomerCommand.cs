using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Customers.Commands.Create;

public class CreateCustomerCommand : IRequest<CustomerInfo>
{
    public CreateCustomerCommand(CustomerInfo customerInfo)
    {
        CustomerInfo = customerInfo;
    }

    public CustomerInfo CustomerInfo { get; init; }
}