using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DomainServices.Features.Categories.Commands.Create;

public class CreateCategoryCommandHandler(IRepository<Category> categoriesRepository, IRepository<Section> sectionsRepository)
    : IRequestHandler<CreateCategoryCommand, Category>
{
    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await ValidateCategoryName(request.Name, cancellationToken);

        List<Section> sections = await ValidateAndGetSectionsAsync(request.SectionsIds, cancellationToken);

        Category newCategory = new Category
        {
            Name = request.Name,
            Sections = sections
        };

        Category? insertedCategory = await categoriesRepository.InsertAsync(newCategory, cancellationToken);

        await categoriesRepository.SaveChangesAsync(cancellationToken);

        return insertedCategory;
    }

    private async Task<List<Section>> ValidateAndGetSectionsAsync(ICollection<long> sectionsIds,
                                                                  CancellationToken cancellationToken = default)
    {
        IList<Section>? existingSections = await sectionsRepository
                                               .GetAllAsync
                                                   (predicate: x => sectionsIds.Contains(x.Id),
                                                    cancellationToken: cancellationToken);

        if (existingSections.Count != sectionsIds.Count)
        {
            IEnumerable<long> missingSectionsIds = sectionsIds.Except(existingSections.Select(x => x.Id));
            string missingSectionsMessage = string.Join(',', missingSectionsIds);

            throw new EntityNotFoundException($"Sections with ids:{missingSectionsMessage} doesn't exist.");
        }

        return existingSections.ToList();
    }

    private async Task ValidateCategoryName(string name, CancellationToken cancellationToken = default)
    {
        bool nameExists = await categoriesRepository.ExistsAsync(x => x.Name == name, cancellationToken);

        if (nameExists)
        {
            throw new ValidationException
                (new[]
                {
                    new ValidationFailure("Category.Name", "Such category name already exists!")
                });
        }
    }
}