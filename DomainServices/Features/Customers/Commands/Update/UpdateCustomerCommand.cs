using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Customers.Commands.Update;

public class UpdateCustomerCommand : IRequest<Unit>
{
    public UpdateCustomerCommand(CustomerInfo customerInfo)
    {
        CustomerInfo = customerInfo;
    }

    public CustomerInfo CustomerInfo { get; init; }
}