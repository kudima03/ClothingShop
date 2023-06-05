using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Users.Commands.Update;

public class UpdateUserCommand : IRequest<Unit>
{
    public UpdateUserCommand(User user)
    {
        User = user;
    }

    public User User { get; init; }
}