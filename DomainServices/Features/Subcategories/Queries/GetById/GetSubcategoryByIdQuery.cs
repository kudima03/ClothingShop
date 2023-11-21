using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Subcategories.Queries.GetById;

public class GetSubcategoryByIdQuery(long id) : IRequest<Subcategory>
{
    public long Id { get; init; } = id;
}