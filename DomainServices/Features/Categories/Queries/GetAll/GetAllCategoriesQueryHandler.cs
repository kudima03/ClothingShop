using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Categories.Queries.GetAll;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    private readonly IReadOnlyRepository<Category> _categoriesRepository;

    public GetAllCategoriesQueryHandler(IReadOnlyRepository<Category> categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request,
                                                    CancellationToken cancellationToken)
    {
        return await _categoriesRepository.GetAllAsync(cancellationToken);
    }
}