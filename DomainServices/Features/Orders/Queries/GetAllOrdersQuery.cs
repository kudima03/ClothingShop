using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.Orders.Queries;

public class GetAllOrdersQuery : EntityCollectionQuery<Order>
{
    public GetAllOrdersQuery() :
        base(new Specification<Order, Order>(order => order))
    {
    }
}