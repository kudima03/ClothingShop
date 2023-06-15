using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IRepository<User> _usersRepository;

    public DeleteUserCommandHandler(IRepository<User> usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User userToDelete = await ValidateAndGetUser(request.Id, cancellationToken);

        userToDelete.DeletionDateTime = DateTime.UtcNow;

        await _usersRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<User> ValidateAndGetUser(long userId, CancellationToken cancellationToken = default)
    {
        User? user = await _usersRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == userId,
            cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException($"{nameof(User)} with id:{userId} doesn't exist.");
        }

        return user;
    }
}