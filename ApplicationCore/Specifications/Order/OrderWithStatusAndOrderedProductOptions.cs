﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Order;

public class OrderWithStatusAndOrderedProductOptions(Expression<Func<Entities.Order, bool>>? predicate = null)
    : Specification<Entities.Order, Entities.Order>(order => order,
                                                    predicate,
                                                    include: orders => orders
                                                                       .Include(order => order.OrderStatus)
                                                                       .Include(order => order.OrderedProductsOptionsInfo));