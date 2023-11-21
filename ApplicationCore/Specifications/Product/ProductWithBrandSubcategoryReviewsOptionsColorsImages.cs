using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Product;

public class ProductWithBrandSubcategoryReviewsOptionsColorsImages(Expression<Func<Entities.Product, bool>>? predicate = null)
    : Specification<Entities.Product, Entities.Product>(x => x,
                                                        predicate,
                                                        x => x.OrderBy(c => c.Name),
                                                        x =>
                                                            x.Include(z => z.Brand)
                                                             .Include(z => z.ProductOptions)
                                                             .ThenInclude(z => z.ProductColor)
                                                             .ThenInclude(z => z.ImagesInfos)
                                                             .Include(z => z.Subcategory)
                                                             .Include(z => z.Reviews)
                                                             .Include(z => z.ProductOptions)
                                                             .ThenInclude(c => c.ReservedProductOptions));