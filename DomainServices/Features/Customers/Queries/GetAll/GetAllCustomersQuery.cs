using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Customers.Queries.GetAll;

public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerInfo>>
{
}