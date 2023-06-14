using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Categories.Commands.Delete;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly IRepository<Category> _categoriesRepository;

    public DeleteCategoryCommandHandler(IRepository<Category> categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _categoriesRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
                                                                                cancellationToken: cancellationToken);

        if (category is not null)
        {
            _categoriesRepository.Delete(category);
            await _categoriesRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}