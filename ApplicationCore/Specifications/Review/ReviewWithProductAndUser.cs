using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Review;

public class ReviewWithProductAndUser : Specification<Entities.Review, Entities.Review>
{
    public ReviewWithProductAndUser(Expression<Func<Entities.Review, bool>>? predicate = null)
        : base(x => x,
               predicate,
               x => x.OrderByDescending(c => c.DateTime),
               reviews => reviews
                          .Include(review => review.User)
                          .Include(review => review.Product)) { }
}