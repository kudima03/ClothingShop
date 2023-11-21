using Infrastructure.Identity.Interfaces;
using MediatR;

namespace Infrastructure.Identity.Features.SignIn;

public class SignInCommandHandler(IAuthorizationService authorizationService) : IRequestHandler<SignInCommand, Unit>
{
    public async Task<Unit> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        await authorizationService.SignInAsync(request.Email, request.Password);

        return Unit.Value;
    }
}