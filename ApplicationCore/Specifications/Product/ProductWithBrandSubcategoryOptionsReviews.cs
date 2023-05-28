using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Product;

public class ProductWithBrandSubcategoryOptionsReviews : Specification<Entities.Product, Entities.Product>
{
    public ProductWithBrandSubcategoryOptionsReviews(Expression<Func<Entities.Product, bool>>? predicate = null)
        : base(x => x,
            predicate,
            include: x =>
                x.Include(z => z.Brand)
                    .Include(z => z.ProductOptions)
                    .Include(z => z.Subcategory)
                    .Include(z => z.Reviews))
    {
    }
}