using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.ProductColor;

public class ProductColorWithImagesInfos : Specification<Entities.ProductColor, Entities.ProductColor>
{
    public ProductColorWithImagesInfos(Expression<Func<Entities.ProductColor, bool>>? predicate = null)
        : base(x => x,
               predicate,
               include: colors => colors
                   .Include(productColor => productColor.ImagesInfos)) { }
}