using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Categories.Commands.Update;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly IRepository<Category> _categoriesRepository;

    private readonly IRepository<Section> _sectionsRepository;

    public UpdateCategoryCommandHandler(IRepository<Category> categoriesRepository,
        IRepository<Section> sectionsRepository)
    {
        _categoriesRepository = categoriesRepository;
        _sectionsRepository = sectionsRepository;
    }

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
            existingSections.Except(existingCategory.Sections, new SectionEqualityComparer());

        IEnumerable<Section> sectionsToRemove =
            existingCategory.Sections.Except(existingSections, new SectionEqualityComparer());

        existingCategory.Sections.RemoveAll(section => sectionsToRemove.Contains(section));

        existingCategory.Sections.AddRange(sectionsToAdd);

        try
        {
            await _categoriesRepository.SaveChangesAsync(cancellationToken);
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
        Category? category = await _categoriesRepository.GetFirstOrDefaultAsync(
            x => x.Id == id,
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
        bool nameExists = await _categoriesRepository.ExistsAsync(x => x.Name == name, cancellationToken);
        if (nameExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("Category.Name", "Such category name already exists!")
            });
        }
    }

    private async Task<IList<Section>> ValidateAndGetSectionsAsync(long[] sectionsIds,
        CancellationToken cancellationToken = default)
    {
        IList<Section>? existingSections = await _sectionsRepository
            .GetAllAsync(predicate: x => sectionsIds.Contains(x.Id), cancellationToken: cancellationToken);
        if (existingSections.Count != sectionsIds.Count())
        {
            throw new EntityNotFoundException("One of sections doesn't exist");
        }

        return existingSections;
    }

    private class SectionEqualityComparer : IEqualityComparer<Section>
    {
        public bool Equals(Section? x, Section? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.Id == y.Id;
        }

        public int GetHashCode(Section obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}