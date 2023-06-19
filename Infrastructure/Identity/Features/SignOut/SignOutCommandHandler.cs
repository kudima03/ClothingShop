using Infrastructure.Identity.Interfaces;
using MediatR;

namespace Infrastructure.Identity.Features.SignOut;
public class SignOutCommandHandler : IRequestHandler<SignOutCommand, Unit>
{
    private readonly IAuthorizationService _authorizationService;

    public SignOutCommandHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        _authorizationService.SingOutAsync();
        return Task.FromResult(Unit.Value);
    }
}
