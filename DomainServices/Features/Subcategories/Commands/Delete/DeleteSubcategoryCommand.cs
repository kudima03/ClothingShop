using MediatR;

namespace DomainServices.Features.Subcategories.Commands.Delete;

public class DeleteSubcategoryCommand : IRequest<Unit>
{
    public DeleteSubcategoryCommand(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}