using MediatR;

namespace Infrastructure.Identity.Features.SignIn;

public class SignInCommand : IRequest<Unit>
{
    public string Email { get; init; }

    public string Password { get; init; }
}