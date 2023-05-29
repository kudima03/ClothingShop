using ApplicationCore.Entities;
using ApplicationCore.Specifications.Review;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;

namespace DomainServices.Features.Reviews.Queries;

public class GetReviewByIdQuery : SingleEntityQuery<Review>
{
    public GetReviewByIdQuery(long id) :
        base(new GetReviewWithProductAndUser(x => x.Id == id))
    {
        Id = id;
    }

    public long Id { get; init; }
}