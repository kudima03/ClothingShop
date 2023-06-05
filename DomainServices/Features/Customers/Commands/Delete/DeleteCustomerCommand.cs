using MediatR;

namespace DomainServices.Features.Customers.Commands.Delete;

public class DeleteCustomerCommand : IRequest<Unit>
{
    public DeleteCustomerCommand(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}