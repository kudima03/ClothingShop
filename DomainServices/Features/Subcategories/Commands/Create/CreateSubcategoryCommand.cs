using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Subcategories.Commands.Create;

public class CreateSubcategoryCommand : IRequest<Subcategory>
{
    public string Name { get; init; }
    public ICollection<long> CategoriesIds { get; init; }
}