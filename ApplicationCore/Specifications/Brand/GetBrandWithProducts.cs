using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications.Brand;
public class GetBrandWithProducts : Specification<Entities.Brand, Entities.Brand>
{
    public GetBrandWithProducts(Expression<Func<Entities.Brand, bool>>? predicate = null)
        : base(selector: brand=>brand,
            predicate, 
            include: brands=>brands.Include(brand=>brand.Products))
    { }
}
