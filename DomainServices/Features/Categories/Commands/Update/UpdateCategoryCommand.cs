using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Categories.Commands.Update;

public class UpdateCategoryCommand : IRequest<Unit>
{
    public UpdateCategoryCommand(Category category)
    {
        Category = category;
    }

    public Category Category { get; init; }
}