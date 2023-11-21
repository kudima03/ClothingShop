using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Brand;

public class GetBrandWithProducts(Expression<Func<Entities.Brand, bool>>? predicate = null)
    : Specification<Entities.Brand, Entities.Brand>(brand => brand,
                                                    predicate,
                                                    x => x.OrderBy(c => c.Name),
                                                    brands => brands.Include(brand => brand.Products));