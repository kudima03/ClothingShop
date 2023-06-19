using Infrastructure.Identity.Entity;
using Infrastructure.Identity.Interfaces;
using MediatR;

namespace Infrastructure.Identity.Features.Register;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IAuthorizationService _authorizationService;

    public RegisterCommandHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        User user = new User()
        {
            UserName = request.Email,
            PasswordHash = request.Password,
            Email = request.Email,
            Name = request.Name,
            Surname = request.Surname,
            Patronymic = request.Patronymic,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
        };

        await _authorizationService.RegisterAsync(user);

        return Unit.Value;
    }
}
