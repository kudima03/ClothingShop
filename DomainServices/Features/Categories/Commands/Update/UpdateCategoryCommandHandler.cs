using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Categories.Commands.Update;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly IRepository<Category> _categoriesRepository;

    public UpdateCategoryCommandHandler(IRepository<Category> categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _categoriesRepository.GetFirstOrDefaultAsync(
            x => x.Id == request.Category.Id,
            categories => categories.Include(category1 => category1.Subcategories),
            cancellationToken);

        if (category is null)
        {
            throw new EntityNotFoundException($"{nameof(Category)} with id:{request.Category.Id} doesn't exist.");
        }

        category.Name = request.Category.Name;
        category.Subcategories.Clear();
        category.Subcategories.AddRange(request.Category.Subcategories);

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
}