using MediatR;

namespace DomainServices.Features.Subcategories.Commands.Update;

public class UpdateSubcategoryCommand : IRequest<Unit>
{
    public long Id { get; init; }

    public string Name { get; init; }

    public ICollection<long> CategoriesIds { get; init; }
}