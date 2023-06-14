using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Users.Commands.Create;

public class CreateUserCommand : IRequest<User>
{
    public long UserTypeId { get; init; }

    public string Email { get; init; }

    public string Password { get; init; }

    public string? Name { get; init; }

    public string? Surname { get; init; }

    public string? Patronymic { get; init; }

    public string? Address { get; init; }

    public string? Phone { get; init; }
}