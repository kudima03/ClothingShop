using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Categories.Queries.GetAll;

public class GetAllCategoriesQueryHandler(IReadOnlyRepository<Category> categoriesRepository) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request,
                                                    CancellationToken cancellationToken)
    {
        return await categoriesRepository.GetAllAsync(cancellationToken);
    }
}