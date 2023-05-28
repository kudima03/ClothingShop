using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.Colors.Queries;

public class GetAllColorsQuery : EntityCollectionQuery<Color>
{
    public GetAllColorsQuery()
        : base(new Specification<Color, Color>(x => x))
    {
    }
}