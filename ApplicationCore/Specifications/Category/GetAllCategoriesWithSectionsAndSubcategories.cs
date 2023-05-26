using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Category;

public class GetAllCategoriesWithSectionsAndSubcategories : ISpecification<Entities.Category, Entities.Category>
{
    public GetAllCategoriesWithSectionsAndSubcategories()
    {
        Selector = category => category;
        Predicate = null;
        OrderBy = x => x.OrderBy(category => category.Id);
        Include = x => x.Include(category => category.Subcategories)
            .Include(category => category.SectionsBelongsTo);
    }

    public Expression<Func<Entities.Category, Entities.Category>> Selector { get; init; }
    public Expression<Func<Entities.Category, bool>> Predicate { get; init; }
    public Func<IQueryable<Entities.Category>, IOrderedQueryable<Entities.Category>> OrderBy { get; init; }
    public Func<IQueryable<Entities.Category>, IIncludableQueryable<Entities.Category, object>> Include { get; init; }
}