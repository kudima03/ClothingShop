using MediatR;

namespace DomainServices.Features.Customers.Commands.Update;

public class UpdateCustomerCommand : IRequest<Unit>
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Patronymic { get; init; }
    public string Address { get; init; }
    public string Phone { get; init; }
}