using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Product;

public class ProductWithBrandSubcategoryOptions : Specification<Entities.Product, Entities.Product>
{
    public ProductWithBrandSubcategoryOptions(Expression<Func<Entities.Product, bool>>? predicate = null)
        : base(x => x,
            predicate,
            orderBy: x => x.OrderBy(c => c.Name),
            include: x =>
                x.Include(z => z.Brand)
                    .Include(z => z.ProductOptions)
                    .Include(z => z.Subcategory))
    {
    }
}