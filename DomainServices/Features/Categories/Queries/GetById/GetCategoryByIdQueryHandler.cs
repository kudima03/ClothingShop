using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Categories.Queries.GetById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
{
    private readonly IReadOnlyRepository<Category> _categoriesRepository;

    public GetCategoryByIdQueryHandler(IReadOnlyRepository<Category> categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        Category? category = await _categoriesRepository
                                   .ApplySpecification
                                       (new CategoryWithSectionsAndSubcategories(category => category.Id == request.Id))
                                   .FirstOrDefaultAsync(cancellationToken);

        if (category is null)
        {
            throw new EntityNotFoundException($"{nameof(Category)} with id:{request.Id} doesn't exist.");
        }

        return category;
    }
}