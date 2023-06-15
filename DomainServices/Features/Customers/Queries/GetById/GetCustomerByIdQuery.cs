using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Customers.Queries.GetById;

public class GetCustomerByIdQuery : IRequest<CustomerInfo>
{
    public GetCustomerByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}