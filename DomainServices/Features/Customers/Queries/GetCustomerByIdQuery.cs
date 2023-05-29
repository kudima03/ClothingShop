using ApplicationCore.Entities;
using ApplicationCore.Specifications.CustomerInfo;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;

namespace DomainServices.Features.Customers.Queries;

public class GetCustomerByIdQuery : SingleEntityQuery<CustomerInfo>
{
    public GetCustomerByIdQuery(long id)
        : base(new CustomerInfoWithUser(x => x.Id == id))
    {
        Id = id;
    }

    public long Id { get; init; }
}