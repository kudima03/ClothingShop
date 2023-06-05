using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Users.Queries.GetById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly IReadOnlyRepository<User> _userRepository;

    public GetUserByIdQueryHandler(IReadOnlyRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
                   cancellationToken: cancellationToken) ??
               throw new EntityNotFoundException($"{nameof(User)} with id:{request.Id} doesn't exist.");
    }
}