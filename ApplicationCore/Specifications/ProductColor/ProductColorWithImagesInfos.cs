using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.ProductColor;

public class ProductColorWithImagesInfos(Expression<Func<Entities.ProductColor, bool>>? predicate = null)
    : Specification<Entities.ProductColor, Entities.ProductColor>(x => x,
                                                                  predicate,
                                                                  include: colors => colors
                                                                      .Include(productColor => productColor.ImagesInfos));