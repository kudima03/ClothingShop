using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Subcategories.Queries.GetById;

public class GetSubcategoryByIdQuery : IRequest<Subcategory>
{
    public GetSubcategoryByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}