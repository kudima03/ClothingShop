using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.Subcategories.Queries;

public class GetAllSubcategoriesQuery : EntityCollectionQuery<Subcategory>
{
    public GetAllSubcategoriesQuery()
        : base(new Specification<Subcategory, Subcategory>(x => x))
    {
    }
}