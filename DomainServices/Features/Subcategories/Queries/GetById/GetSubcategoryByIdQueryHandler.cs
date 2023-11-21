using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Subcategories.Queries.GetById;

public class GetSubcategoryByIdQueryHandler(IReadOnlyRepository<Subcategory> subcategoriesRepository) : IRequestHandler<GetSubcategoryByIdQuery, Subcategory>
{
    public async Task<Subcategory> Handle(GetSubcategoryByIdQuery request, CancellationToken cancellationToken)
    {
        Subcategory? subcategory = await subcategoriesRepository.GetFirstOrDefaultAsync
                                       (predicate: x => x.Id == request.Id,
                                        cancellationToken: cancellationToken);

        if (subcategory is null)
        {
            throw new EntityNotFoundException($"{nameof(Subcategory)} with id:{request.Id} doesn't exist.");
        }

        return subcategory;
    }
}