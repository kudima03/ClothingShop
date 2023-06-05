using MediatR;

namespace DomainServices.Features.ProductColors.Commands.Delete;

public class DeleteProductColorCommand : IRequest<Unit>
{
    public DeleteProductColorCommand(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}