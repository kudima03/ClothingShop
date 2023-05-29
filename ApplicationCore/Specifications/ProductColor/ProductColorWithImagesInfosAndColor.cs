using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.ProductColor;

public class ProductColorWithImagesInfosAndColor : Specification<Entities.ProductColor, Entities.ProductColor>
{
    public ProductColorWithImagesInfosAndColor(Expression<Func<Entities.ProductColor, bool>>? predicate = null)
        : base(x => x,
            predicate,
            include: colors => colors
                .Include(productColor => productColor.Color)
                .Include(productColor => productColor.ImagesInfos)
        )
    {
    }
}