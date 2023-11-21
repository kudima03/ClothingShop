using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Subcategories.Queries.GetAll;

internal class GetAllSubcategoriesQueryHandler(IReadOnlyRepository<Subcategory> subcategoriesRepository) : IRequestHandler<GetAllSubcategoriesQuery, IEnumerable<Subcategory>>
{
    public async Task<IEnumerable<Subcategory>> Handle(GetAllSubcategoriesQuery request,
                                                       CancellationToken cancellationToken)
    {
        return await subcategoriesRepository.GetAllAsync(cancellationToken);
    }
}