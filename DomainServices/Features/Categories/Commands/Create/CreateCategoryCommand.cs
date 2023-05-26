using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Categories.Commands.Create;

public class CreateCategoryCommand : IRequest<Category>
{
    public CreateCategoryCommand(Category category)
    {
        Category = category;
    }

    public Category Category { get; init; }
}