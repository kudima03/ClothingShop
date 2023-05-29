using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Review;

public class GetReviewWithProductAndUser : Specification<Entities.Review, Entities.Review>
{
    public GetReviewWithProductAndUser(Expression<Func<Entities.Review, bool>>? predicate = null)
        : base(x => x,
            predicate,
            include: reviews => reviews
                .Include(review => review.User)
                .Include(review => review.Product))
    {
    }
}