using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Category;

public class CategoryWithSectionsAndSubcategories : Specification<Entities.Category, Entities.Category>
{
    public CategoryWithSectionsAndSubcategories(Expression<Func<Entities.Category, bool>>? predicate = null)
        : base
            (category => category,
             predicate,
             categories => categories.OrderBy(category => category.Name),
             categories => categories.Include(category => category.Subcategories)
                                     .Include(category => category.Sections)) { }
}