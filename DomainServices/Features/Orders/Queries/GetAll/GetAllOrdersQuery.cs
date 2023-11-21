using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Orders.Queries.GetAll;

public class GetAllOrdersQuery : IRequest<IEnumerable<Order>>;