using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Subcategories.Queries.GetById;

public class GetSubcategoryByIdQueryHandler : IRequestHandler<GetSubcategoryByIdQuery, Subcategory>
{
    private readonly IReadOnlyRepository<Subcategory> _subcategoriesRepository;

    public GetSubcategoryByIdQueryHandler(IReadOnlyRepository<Subcategory> subcategoriesRepository)
    {
        _subcategoriesRepository = subcategoriesRepository;
    }

    public async Task<Subcategory> Handle(GetSubcategoryByIdQuery request, CancellationToken cancellationToken)
    {
        Subcategory? subcategory = await _subcategoriesRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (subcategory is null)
        {
            throw new EntityNotFoundException($"{nameof(Subcategory)} with id:{request.Id} doesn't exist.");
        }

        return subcategory;
    }
}