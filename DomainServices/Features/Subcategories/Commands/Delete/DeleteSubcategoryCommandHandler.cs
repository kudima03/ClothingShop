using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Subcategories.Commands.Delete;

public class DeleteSubcategoryCommandHandler : IRequestHandler<DeleteSubcategoryCommand, Unit>
{
    private readonly IRepository<Subcategory> _subcategoriesRepository;

    public DeleteSubcategoryCommandHandler(IRepository<Subcategory> subcategoriesRepository)
    {
        _subcategoriesRepository = subcategoriesRepository;
    }

    public async Task<Unit> Handle(DeleteSubcategoryCommand request, CancellationToken cancellationToken)
    {
        Subcategory? subcategory = await _subcategoriesRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (subcategory is not null)
        {
            _subcategoriesRepository.Delete(subcategory);
            await _subcategoriesRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}