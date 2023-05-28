using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications.ProductOption;
public class ProductOptionWithColorAndImages : Specification<Entities.ProductOption, Entities.ProductOption>
{
    public ProductOptionWithColorAndImages(Expression<Func<Entities.ProductOption, bool>>? predicate = null)
        : base(selector: productOption => productOption,
            predicate: predicate,
            include: productOptions => 
                productOptions.Include(productOption => productOption.ProductColor)
                    .ThenInclude(productColor => productColor.Color)
                    .Include(productOption => productOption.ProductColor.ImagesInfos))
    { }
}
