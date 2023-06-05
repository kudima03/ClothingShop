using MediatR;

namespace DomainServices.Features.Brands.Commands.Delete;

public class DeleteBrandCommand : IRequest<Unit>
{
    public DeleteBrandCommand(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}