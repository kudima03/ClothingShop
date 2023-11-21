using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Categories.Queries.GetById;

public class GetCategoryByIdQuery(long id) : IRequest<Category>
{
    public long Id { get; init; } = id;
}