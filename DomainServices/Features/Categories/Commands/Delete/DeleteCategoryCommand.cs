using MediatR;

namespace DomainServices.Features.Categories.Commands.Delete;

public class DeleteCategoryCommand(long id) : IRequest<Unit>
{
    public long Id { get; init; } = id;
}