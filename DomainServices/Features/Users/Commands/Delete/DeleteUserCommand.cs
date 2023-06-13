using MediatR;

namespace DomainServices.Features.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<Unit>
{
    public DeleteUserCommand(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}