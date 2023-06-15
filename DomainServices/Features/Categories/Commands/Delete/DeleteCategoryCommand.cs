using MediatR;

namespace DomainServices.Features.Categories.Commands.Delete;

public class DeleteCategoryCommand : IRequest<Unit>
{
    public DeleteCategoryCommand(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}