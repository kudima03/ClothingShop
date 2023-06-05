using ApplicationCore.Entities;
using ApplicationCore.Specifications.CustomerInfo;
using MediatR;

namespace DomainServices.Features.Customers.Queries.GetById;

public class GetCustomerByIdQuery : IRequest<CustomerInfo?>
{
    public GetCustomerByIdQuery(long id)
    {
        Specification = new CustomerInfoWithUser(customerInfo => customerInfo.Id == id);
        Id = id;
    }

    public CustomerInfoWithUser Specification { get; init; }
    public long Id { get; init; }
}