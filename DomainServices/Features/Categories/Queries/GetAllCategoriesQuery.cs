using ApplicationCore.Entities;
using ApplicationCore.Specifications.Category;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.Categories.Queries;

public class GetAllCategoriesQuery : EntityCollectionQuery<Category>
{
    public GetAllCategoriesQuery() :
        base(new GetAllCategoriesWithSectionsAndSubcategories())
    {
    }
}