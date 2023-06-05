using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Subcategories.Commands.Update;

public class UpdateSubcategoryCommand : IRequest<Unit>
{
    public UpdateSubcategoryCommand(Subcategory subcategory)
    {
        Subcategory = subcategory;
    }

    public Subcategory Subcategory { get; init; }
}