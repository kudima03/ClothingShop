using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Categories.Commands.DeassociateSubcategory;

public class
    DeassociateSubcategoryWithCategoryCommandHandler : IRequestHandler<DeassociateSubcategoryWithCategoryCommand, Unit>
{
    private readonly IRepository<Category> _categoriesRepository;
    private readonly IRepository<Subcategory> _subcategoriesRepository;

    public DeassociateSubcategoryWithCategoryCommandHandler(IRepository<Subcategory> subcategoriesRepository,
        IRepository<Category> categoriesRepository)
    {
        _subcategoriesRepository = subcategoriesRepository;
        _categoriesRepository = categoriesRepository;
    }

    public async Task<Unit> Handle(DeassociateSubcategoryWithCategoryCommand request,
        CancellationToken cancellationToken)
    {
        (Category category, Subcategory subcategory) =
            await ValidateAndGetValues(request.CategoryId, request.SubcategoryId, cancellationToken);

        if (category.Subcategories.Contains(subcategory))
        {
            category.Subcategories.Remove(subcategory);
            await _categoriesRepository.SaveChangesAsync(cancellationToken);
        }

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