using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Sections.Commands.Delete;

public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand>
{
    private readonly IRepository<Section> _sectionsRepository;

    public DeleteSectionCommandHandler(IRepository<Section> sectionsRepository)
    {
        _sectionsRepository = sectionsRepository;
    }

    public async Task Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        _sectionsRepository.Delete(
            (await _sectionsRepository.GetFirstOrDefaultAsync(predicate: section => section.Id == request.Id,
                cancellationToken: cancellationToken))!);
        await _sectionsRepository.SaveChangesAsync(cancellationToken);
    }
}