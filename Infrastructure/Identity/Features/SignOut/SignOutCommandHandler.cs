using Infrastructure.Identity.Interfaces;
using MediatR;

namespace Infrastructure.Identity.Features.SignOut;

public class SignOutCommandHandler(IAuthorizationService authorizationService) : IRequestHandler<SignOutCommand, Unit>
{
    public Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        authorizationService.SingOutAsync();

        return Task.FromResult(Unit.Value);
    }
}