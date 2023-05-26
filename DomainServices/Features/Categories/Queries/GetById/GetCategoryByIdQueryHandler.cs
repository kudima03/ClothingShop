using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Categories.Queries.GetById;

internal class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
{
    private readonly IReadOnlyRepository<Category> _categoriesRepository;

    public GetCategoryByIdQueryHandler(IReadOnlyRepository<Category> categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return (await _categoriesRepository
            .ApplySpecification(new GetCategoryByIdWithSectionsAndSubcategories(request.Id))
            .FirstOrDefaultAsync(cancellationToken))!;
    }
}