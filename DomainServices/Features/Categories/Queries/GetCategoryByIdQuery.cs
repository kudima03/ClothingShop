using ApplicationCore.Entities;
using ApplicationCore.Specifications.Category;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;

namespace DomainServices.Features.Categories.Queries;

public class GetCategoryByIdQuery : SingleEntityQuery<Category>
{
    public GetCategoryByIdQuery(long id) :
        base(new GetCategoryByIdWithSectionsAndSubcategories(id))
    {
        Id = id;
    }

    public long Id { get; init; }
}