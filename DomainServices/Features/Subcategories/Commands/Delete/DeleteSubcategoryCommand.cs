using MediatR;

namespace DomainServices.Features.Subcategories.Commands.Delete;

public class DeleteSubcategoryCommand(long id) : IRequest<Unit>
{
    public long Id { get; init; } = id;
}