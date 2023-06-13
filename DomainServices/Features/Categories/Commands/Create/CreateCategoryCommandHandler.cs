using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DomainServices.Features.Categories.Commands.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
{
    private readonly IRepository<Category> _categoriesRepository;
    private readonly IRepository<Section> _sectionsRepository;

    public CreateCategoryCommandHandler(IRepository<Category> categoriesRepository, IRepository<Section> sectionsRepository)
    {
        _categoriesRepository = categoriesRepository;
        _sectionsRepository = sectionsRepository;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await ValidateCategoryName(request.Name, cancellationToken);

        List<Section> sections = await ValidateAndGetSectionsAsync(request.SectionsIds, cancellationToken);

        Category newCategory = new()
        {
            Name = request.Name,
            Sections = sections
        };

        Category? insertedCategory = await _categoriesRepository.InsertAsync(newCategory, cancellationToken);

        await _categoriesRepository.SaveChangesAsync(cancellationToken);

        return insertedCategory;
    }

    private async Task<List<Section>> ValidateAndGetSectionsAsync(ICollection<long> sectionsIds,
        CancellationToken cancellationToken = default)
    {
        IList<Section>? existingSections = await _sectionsRepository
            .GetAllAsync(predicate: x => sectionsIds.Contains(x.Id), cancellationToken: cancellationToken);
        if (existingSections.Count != sectionsIds.Count)
        {
            throw new EntityNotFoundException("One of sections doesn't exist");
        }

        return existingSections.ToList();
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