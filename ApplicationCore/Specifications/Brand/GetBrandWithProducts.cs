using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Brand;

public class GetBrandWithProducts : Specification<Entities.Brand, Entities.Brand>
{
    public GetBrandWithProducts(Expression<Func<Entities.Brand, bool>>? predicate = null)
        : base(brand => brand,
               predicate,
               x => x.OrderBy(c => c.Name),
               brands => brands.Include(brand => brand.Products)) { }
}