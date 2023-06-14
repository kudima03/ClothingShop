using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Order;

public class OrderWithStatusAndOrderedProductOptions : Specification<Entities.Order, Entities.Order>
{
    public OrderWithStatusAndOrderedProductOptions(Expression<Func<Entities.Order, bool>>? predicate = null)
        : base(order => order,
               predicate,
               include: orders => orders
                                  .Include(order => order.OrderStatus)
                                  .Include(order => order.OrderedProductsOptionsInfo)) { }
}