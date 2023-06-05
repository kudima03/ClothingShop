using MediatR;

namespace DomainServices.Features.ProductsOptions.Commands.Delete;

public class DeleteProductOptionCommand : IRequest<Unit>
{
    public DeleteProductOptionCommand(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}