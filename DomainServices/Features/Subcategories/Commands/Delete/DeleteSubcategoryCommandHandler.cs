using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Subcategories.Commands.Delete;

public class DeleteSubcategoryCommandHandler(IRepository<Subcategory> subcategoriesRepository) : IRequestHandler<DeleteSubcategoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteSubcategoryCommand request, CancellationToken cancellationToken)
    {
        Subcategory? subcategory = await subcategoriesRepository.GetFirstOrDefaultAsync
                                       (predicate: x => x.Id == request.Id,
                                        cancellationToken: cancellationToken);

        if (subcategory is not null)
        {
            subcategoriesRepository.Delete(subcategory);
            await subcategoriesRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}