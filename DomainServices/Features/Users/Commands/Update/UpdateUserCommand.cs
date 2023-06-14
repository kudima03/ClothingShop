using MediatR;

namespace DomainServices.Features.Users.Commands.Update;

public class UpdateUserCommand : IRequest<Unit>
{
    public long Id { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public long UserTypeId { get; init; }
}