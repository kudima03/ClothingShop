using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.ProductOption;

public class ProductOptionWithColorAndImages : Specification<Entities.ProductOption, Entities.ProductOption>
{
    public ProductOptionWithColorAndImages(Expression<Func<Entities.ProductOption, bool>>? predicate = null)
        : base(productOption => productOption,
            predicate,
            include: productOptions =>
                productOptions.Include(productOption => productOption.ProductColor)
                    .ThenInclude(productColor => productColor.Color)
                    .Include(productOption => productOption.ProductColor.ImagesInfos))
    {
    }
}