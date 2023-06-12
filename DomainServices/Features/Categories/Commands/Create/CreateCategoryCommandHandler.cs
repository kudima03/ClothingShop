using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
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
        await ValidateCategoryName(request.Name, cancellationToken);

        Category newCategory = new() { Name = request.Name };

        Category? insertedCategory = await _categoriesRepository.InsertAsync(newCategory, cancellationToken);
        await _categoriesRepository.SaveChangesAsync(cancellationToken);
        return insertedCategory;
    }

    private async Task ValidateCategoryName(string name, CancellationToken cancellationToken = default)
    {
        bool nameExists = await _categoriesRepository.ExistsAsync(x => x.Name == name, cancellationToken);

        if (nameExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("Category.Name", "Such category name already exists!")
            });
        }
    }
}