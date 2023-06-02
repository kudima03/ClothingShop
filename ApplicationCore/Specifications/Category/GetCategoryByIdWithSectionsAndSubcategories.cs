using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Specifications.Category;

public class GetCategoryByIdWithSectionsAndSubcategories
    : Specification<Entities.Category, Entities.Category>
{
    public GetCategoryByIdWithSectionsAndSubcategories(long id)
        : base(category => category, category => category.Id == id,
            x => x.OrderBy(category => category.Id),
            x => x.Include(category => category.Subcategories)
                .Include(category => category.Sections))
    {
    }
}