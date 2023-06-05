using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications.Product;
public class ProductWithBrandSubcategoryOptions : Specification<Entities.Product, Entities.Product>
{
    public ProductWithBrandSubcategoryOptions(Expression<Func<Entities.Product, bool>>? predicate = null)
        : base(x => x,
            predicate,
            include: x =>
                x.Include(z => z.Brand)
                    .Include(z => z.ProductOptions)
                    .Include(z => z.Subcategory))
    {
    }
}