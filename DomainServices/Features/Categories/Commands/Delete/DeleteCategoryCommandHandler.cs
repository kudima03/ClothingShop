using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Categories.Commands.Delete;

internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IRepository<Section> _sectionsRepository;

    public DeleteCategoryCommandHandler(IRepository<Section> sectionsRepository)
    {
        _sectionsRepository = sectionsRepository;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        _sectionsRepository.Delete(
            (await _sectionsRepository.GetFirstOrDefaultAsync(
                predicate: x => x.Id == request.Id, cancellationToken: cancellationToken))!);
        await _sectionsRepository.SaveChangesAsync(cancellationToken);
    }
}