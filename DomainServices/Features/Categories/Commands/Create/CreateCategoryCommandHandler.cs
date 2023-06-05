using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Categories.Commands.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
{
    private readonly IRepository<Category> _categoriesRepository;

    public CreateCategoryCommandHandler(IRepository<Category> categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _categoriesRepository.InsertAsync(request.Category, cancellationToken);
        await _categoriesRepository.SaveChangesAsync(cancellationToken);
        return category;
    }
}