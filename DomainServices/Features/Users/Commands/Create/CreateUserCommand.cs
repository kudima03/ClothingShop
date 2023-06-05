using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Users.Commands.Create;

public class CreateUserCommand : IRequest<User>
{
    public CreateUserCommand(User user)
    {
        User = user;
    }

    public User User { get; init; }
}