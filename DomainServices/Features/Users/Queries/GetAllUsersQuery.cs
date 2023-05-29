using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.Users.Queries;

public class GetAllUsersQuery : EntityCollectionQuery<User>
{
    public GetAllUsersQuery()
        : base(new Specification<User, User>(user => user))
    {
    }
}