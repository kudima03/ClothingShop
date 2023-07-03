using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Subcategories.Queries.GetAll;

internal class GetAllSubcategoriesQueryHandler : IRequestHandler<GetAllSubcategoriesQuery, IEnumerable<Subcategory>>
{
    private readonly IReadOnlyRepository<Subcategory> _subcategoriesRepository;

    public GetAllSubcategoriesQueryHandler(IReadOnlyRepository<Subcategory> subcategoriesRepository)
    {
        _subcategoriesRepository = subcategoriesRepository;
    }

    public async Task<IEnumerable<Subcategory>> Handle(GetAllSubcategoriesQuery request,
                                                       CancellationToken cancellationToken)
    {
        return await _subcategoriesRepository.GetAllAsync(cancellationToken);
    }
}