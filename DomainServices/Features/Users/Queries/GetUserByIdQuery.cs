using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Users.Queries;

public class GetUserByIdQuery : SingleEntityQuery<User>
{
    public GetUserByIdQuery(long id) :
        base(new Specification<User, User>(user => user,
            user => user.Id == id,
            include: users => users.Include(user => user.UserType)))
    {
        Id = id;
    }

    public long Id { get; init; }
}