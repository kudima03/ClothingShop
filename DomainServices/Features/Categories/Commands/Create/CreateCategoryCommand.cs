using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Categories.Commands.Create;

public class CreateCategoryCommand : IRequest<Category>
{
    public string Name { get; init; }
}