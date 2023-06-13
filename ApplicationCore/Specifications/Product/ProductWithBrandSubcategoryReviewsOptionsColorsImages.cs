using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Product;

public class ProductWithBrandSubcategoryReviewsOptionsColorsImages : Specification<Entities.Product, Entities.Product>
{
    public ProductWithBrandSubcategoryReviewsOptionsColorsImages(
        Expression<Func<Entities.Product, bool>>? predicate = null)
        : base(x => x,
            predicate,
            orderBy: x => x.OrderBy(c => c.Name),
            include: x =>
                x.Include(z => z.Brand)
                    .Include(z => z.ProductOptions)
                    .ThenInclude(z => z.ProductColor)
                    .ThenInclude(z => z.ImagesInfos)
                    .Include(z => z.Subcategory)
                    .Include(z => z.Reviews))
    {
    }
}