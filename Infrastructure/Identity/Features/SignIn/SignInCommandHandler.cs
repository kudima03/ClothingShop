using Infrastructure.Identity.Interfaces;
using MediatR;

namespace Infrastructure.Identity.Features.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, Unit>
{
    private readonly IAuthorizationService _authorizationService;

    public SignInCommandHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<Unit> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        await _authorizationService.SignInAsync(request.Email, request.Password);

        return Unit.Value;
    }
}