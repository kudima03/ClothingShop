using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Users.Queries.GetById;

public class GetUserByIdQuery : IRequest<User>
{
    public GetUserByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}