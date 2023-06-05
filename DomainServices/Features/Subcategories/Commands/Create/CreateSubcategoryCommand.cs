using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Subcategories.Commands.Create;

public class CreateSubcategoryCommand : IRequest<Subcategory>
{
    public CreateSubcategoryCommand(Subcategory subcategory)
    {
        Subcategory = subcategory;
    }

    public Subcategory Subcategory { get; init; }
}