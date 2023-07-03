using MediatR;

namespace DomainServices.Features.Categories.Commands.Update;

public class UpdateCategoryCommand : IRequest<Unit>
{
    public long Id { get; init; }

    public string Name { get; init; }

    public ICollection<long> SectionsIds { get; init; }
}