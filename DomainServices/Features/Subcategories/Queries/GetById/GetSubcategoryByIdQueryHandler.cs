using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Subcategories.Queries.GetById;

public class GetSubcategoryByIdQueryHandler : IRequestHandler<GetSubcategoryByIdQuery, Subcategory?>
{
    private readonly IReadOnlyRepository<Subcategory> _subcategoriesRepository;

    public GetSubcategoryByIdQueryHandler(IReadOnlyRepository<Subcategory> subcategoriesRepository)
    {
        _subcategoriesRepository = subcategoriesRepository;
    }

    public async Task<Subcategory?> Handle(GetSubcategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _subcategoriesRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);
    }
}