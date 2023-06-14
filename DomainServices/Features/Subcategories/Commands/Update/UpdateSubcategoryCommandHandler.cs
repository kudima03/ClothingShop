using ApplicationCore.Entities;
using ApplicationCore.EqualityComparers;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Subcategories.Commands.Update;

public class UpdateSubcategoryCommandHandler : IRequestHandler<UpdateSubcategoryCommand, Unit>
{
    private readonly IRepository<Category> _categoriesRepository;
    private readonly IRepository<Subcategory> _subcategoriesRepository;

    public UpdateSubcategoryCommandHandler(IRepository<Subcategory> subcategoriesRepository,
        IRepository<Category> categoriesRepository)
    {
        _subcategoriesRepository = subcategoriesRepository;
        _categoriesRepository = categoriesRepository;
    }

    public async Task<Unit> Handle(UpdateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        Subcategory existingSubcategory = await ValidateAndGetSubcategoryAsync(request.Id, cancellationToken);

        if (existingSubcategory.Name != request.Name)
        {
            await ValidateSubcategoryNameAsync(request.Name, cancellationToken);
        }

        existingSubcategory.Name = request.Name;

        IList<Category> existingCategories =
            await ValidateAndGetCategoriesAsync(request.CategoriesIds, cancellationToken);

        IEnumerable<Category> categoriesToAdd =
            existingCategories.Except(existingSubcategory.Categories, new CategoryEqualityComparerById());

        IEnumerable<Category> categoriesToRemove =
            existingSubcategory.Categories.Except(existingCategories, new CategoryEqualityComparerById());

        existingSubcategory.Categories.RemoveAll(section => categoriesToRemove.Contains(section));

        existingSubcategory.Categories.AddRange(categoriesToAdd);

        try
        {
            await _subcategoriesRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException(
                $"Unable to perform update {nameof(Subcategory)} operation. Check input.");
        }

        return Unit.Value;
    }

    private async Task<Subcategory> ValidateAndGetSubcategoryAsync(long subcategoryId,
        CancellationToken cancellationToken = default)
    {
        Subcategory? subcategory = await _subcategoriesRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == subcategoryId,
            cancellationToken: cancellationToken);

        if (subcategory is null)
        {
            throw new EntityNotFoundException($"{nameof(Subcategory)} with id:{subcategoryId} doesn't exist.");
        }

        return subcategory;
    }

    private async Task ValidateSubcategoryNameAsync(string name, CancellationToken cancellationToken = default)
    {
        bool nameExists = await _subcategoriesRepository.ExistsAsync(x => x.Name == name, cancellationToken);

        if (nameExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("Subcategory.Name", "Such subcategory name already exists!")
            });
        }
    }

    private async Task<IList<Category>> ValidateAndGetCategoriesAsync(ICollection<long> categoriesIds,
        CancellationToken cancellationToken = default)
    {
        IList<Category>? existingCategories = await _categoriesRepository
            .GetAllAsync(predicate: x => categoriesIds.Contains(x.Id), cancellationToken: cancellationToken);
        if (existingCategories.Count != categoriesIds.Count)
        {
            IEnumerable<long> missingCategoriesIds = categoriesIds.Except(existingCategories.Select(x => x.Id));
            string missingCategoriesMessage = string.Join(',', missingCategoriesIds);
            throw new EntityNotFoundException($"Categories with ids:{missingCategoriesMessage} doesn't exist.");
        }

        return existingCategories;
    }
}