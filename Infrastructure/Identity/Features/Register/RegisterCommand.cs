using MediatR;

namespace Infrastructure.Identity.Features.Register;
public class RegisterCommand : IRequest<Unit>
{
    public string Email { get; init; }
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string? Patronymic { get; init; }
    public string? Address { get; init; }
    public string? PhoneNumber { get; init; }

    public RegisterCommand()
    {
        
    }
}