using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.Customers.Queries;

public class GetAllCustomersQuery : EntityCollectionQuery<CustomerInfo>
{
    public GetAllCustomersQuery()
        : base(new Specification<CustomerInfo, CustomerInfo>(customerInfo => customerInfo))
    {
    }
}