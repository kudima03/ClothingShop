using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Users.Queries.GetAll;

public class GetAllUsersQuery : IRequest<IEnumerable<User>>
{
}