using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.ProductOption;

public class ProductOptionWithProductColorAndImages : Specification<Entities.ProductOption, Entities.ProductOption>
{
    public ProductOptionWithProductColorAndImages(Expression<Func<Entities.ProductOption, bool>>? predicate = null)
        : base
            (productOption => productOption,
             predicate,
             include: productOptions =>
                 productOptions.Include(productOption => productOption.ProductColor)
                               .Include(productOption => productOption.ProductColor.ImagesInfos)) { }
}