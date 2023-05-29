using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.ProductColors.Queries;

public class GetAllProductColorsQuery : EntityCollectionQuery<ProductColor>
{
    public GetAllProductColorsQuery()
        : base(new Specification<ProductColor, ProductColor>(productColor => productColor))
    {
    }
}