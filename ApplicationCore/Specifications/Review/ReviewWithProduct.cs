using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Review;

public class ReviewWithProduct : Specification<Entities.Review, Entities.Review>
{
    public ReviewWithProduct(Expression<Func<Entities.Review, bool>>? predicate = null)
        : base(x => x,
            predicate,
            x => x.OrderByDescending(c => c.DateTime),

            reviews => reviews
                .Include(review => review.Product))
    {
    }
}