using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Specifications.Category;

public class GetAllCategoriesWithSectionsAndSubcategories : Specification<Entities.Category, Entities.Category>
{
    public GetAllCategoriesWithSectionsAndSubcategories()
        : base(category => category, null,
            x => x.OrderBy(category => category.Id),
            x => x.Include(category => category.Subcategories)
                .Include(category => category.SectionsBelongsTo))
    {
    }
}