using ApplicationCore.Entities;
using ApplicationCore.Specifications.Category;
using MediatR;

namespace DomainServices.Features.Categories.Queries.GetById;

public class GetCategoryByIdQuery : IRequest<Category?>
{
    public GetCategoryByIdQuery(long id)
    {
        Specification = new CategoryWithSectionsAndSubcategories(category => category.Id == id);
    }

    public CategoryWithSectionsAndSubcategories Specification { get; init; }

    public long Id { get; init; }
}