using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Categories.Commands.Delete;

public class DeleteCategoryCommandHandler(IRepository<Category> categoriesRepository) : IRequestHandler<DeleteCategoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoriesRepository.GetFirstOrDefaultAsync
                                 (predicate: x => x.Id == request.Id,
                                  cancellationToken: cancellationToken);

        if (category is not null)
        {
            categoriesRepository.Delete(category);
            await categoriesRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}