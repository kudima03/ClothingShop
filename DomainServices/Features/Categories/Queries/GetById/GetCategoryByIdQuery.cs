using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Categories.Queries.GetById;

public class GetCategoryByIdQuery : IRequest<Category>
{
    public GetCategoryByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}