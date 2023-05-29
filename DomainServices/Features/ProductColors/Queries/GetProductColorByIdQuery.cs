using ApplicationCore.Entities;
using ApplicationCore.Specifications.ProductColor;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;

namespace DomainServices.Features.ProductColors.Queries;

public class GetProductColorByIdQuery : SingleEntityQuery<ProductColor>
{
    public GetProductColorByIdQuery(long id)
        : base(new ProductColorWithImagesInfosAndColor(x => x.Id == id))
    {
        Id = id;
    }

    public long Id { get; init; }
}