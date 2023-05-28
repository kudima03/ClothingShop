using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Categories.Commands.AssociateSubcategory;

public class
    AssociateSubcategoryWithCategoryCommandHandler : IRequestHandler<AssociateSubcategoryWithCategoryCommand, Unit>
{
    private readonly IRepository<Category> _categoriesRepository;
    private readonly IRepository<Subcategory> _subcategoriesRepository;

    public AssociateSubcategoryWithCategoryCommandHandler(IRepository<Category> categoriesRepository,
        IRepository<Subcategory> subcategoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
        _subcategoriesRepository = subcategoriesRepository;
    }

    public async Task<Unit> Handle(AssociateSubcategoryWithCategoryCommand request, CancellationToken cancellationToken)
    {
        (Category category, Subcategory subcategory) =
            await ValidateAndGetValues(request.CategoryId, request.SubcategoryId, cancellationToken);

        category.Subcategories.Add(subcategory);

        await _categoriesRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<(Category category, Subcategory subcategory)> ValidateAndGetValues(long categoryId,
        long subcategoryId,
        CancellationToken cancellationToken = default)
    {
        Category? category = await _categoriesRepository
            .GetFirstOrDefaultAsync(x => x.Id == categoryId,
                categories => categories.Include(sec => sec.Subcategories),
                cancellationToken);

        if (category is null)
        {
            throw new ValidationException("Validation error.",
                new[]
                {
                    new ValidationFailure($"{nameof(category.Id)}",
                        $"Category with id:{categoryId} not found")
                });
        }

        Subcategory? subcategory = await _subcategoriesRepository
            .GetFirstOrDefaultAsync(predicate: x => x.Id == subcategoryId,
                cancellationToken: cancellationToken);
        if (subcategory is null)
        {
            throw new ValidationException("Validation error.",
                new[]
                {
                    new ValidationFailure($"{nameof(subcategory.Id)}",
                        $"Subcategory with id:{subcategoryId} not found")
                });
        }

        return (category, subcategory);
    }
}