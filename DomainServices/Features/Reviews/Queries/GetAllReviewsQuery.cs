using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.Reviews.Queries;

public class GetAllReviewsQuery : EntityCollectionQuery<Review>
{
    public GetAllReviewsQuery() :
        base(new Specification<Review, Review>(review => review))
    {
    }
}