using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IRepository<User> _repository;

    public DeleteUserCommandHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _repository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (user is not null)
        {
            _repository.Delete(user);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}