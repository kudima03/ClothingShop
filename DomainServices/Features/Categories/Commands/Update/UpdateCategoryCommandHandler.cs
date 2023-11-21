using ApplicationCore.Entities;
using ApplicationCore.EqualityComparers;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Categories.Commands.Update;

public class UpdateCategoryCommandHandler(IRepository<Category> categoriesRepository,
                                          IRepository<Section> sectionsRepository)
    : IRequestHandler<UpdateCategoryCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category existingCategory = await ValidateAndGetCategoryToUpdateAsync(request.Id, cancellationToken);

        if (existingCategory.Name != request.Name)
        {
            await ValidateCategoryNameAsync(request.Name, cancellationToken);
        }

        existingCategory.Name = request.Name;

        IList<Section> existingSections = await ValidateAndGetSectionsAsync(request.SectionsIds, cancellationToken);

        IEnumerable<Section> sectionsToAdd =
            existingSections.Except(existingCategory.Sections, new SectionEqualityComparerById());

        IEnumerable<Section> sectionsToRemove =
            existingCategory.Sections.Except(existingSections, new SectionEqualityComparerById());

        existingCategory.Sections.RemoveAll(section => sectionsToRemove.Contains(section));

        existingCategory.Sections.AddRange(sectionsToAdd);

        try
        {
            await categoriesRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform update {nameof(Category)} operation. Check input.");
        }

        return Unit.Value;
    }

    private async Task<Category> ValidateAndGetCategoryToUpdateAsync(long id,
                                                                     CancellationToken cancellationToken = default)
    {
        Category? category = await categoriesRepository.GetFirstOrDefaultAsync
                                 (x => x.Id == id,
                                  categories => categories.Include(cat => cat.Sections),
                                  cancellationToken);

        if (category is null)
        {
            throw new EntityNotFoundException($"{nameof(Category)} with id:{id} doesn't exist.");
        }

        return category;
    }

    private async Task ValidateCategoryNameAsync(string name, CancellationToken cancellationToken = default)
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

    private async Task<IList<Section>> ValidateAndGetSectionsAsync(ICollection<long> sectionsIds,
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

        return existingSections;
    }
}