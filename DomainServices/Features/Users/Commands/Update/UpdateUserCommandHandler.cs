using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Users.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IRepository<User> _userRepository;

    public UpdateUserCommandHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException($"{nameof(User)} with id:{request.Id} doesn't exist.");
        }

        user.Email = request.Email;
        user.Password = request.Password;
        user.UserTypeId = request.UserTypeId;

        try
        {
            await _userRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform update {nameof(User)} operation. Check input.");
        }

        return Unit.Value;
    }
}