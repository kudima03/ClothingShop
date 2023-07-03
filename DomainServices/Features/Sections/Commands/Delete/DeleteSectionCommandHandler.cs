using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Sections.Commands.Delete;

public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, Unit>
{
    private readonly IRepository<Section> _sectionsRepository;

    public DeleteSectionCommandHandler(IRepository<Section> sectionsRepository)
    {
        _sectionsRepository = sectionsRepository;
    }

    public async Task<Unit> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        Section? section = await _sectionsRepository.GetFirstOrDefaultAsync
                               (predicate: x => x.Id == request.Id,
                                cancellationToken: cancellationToken);

        if (section is not null)
        {
            _sectionsRepository.Delete(section);
            await _sectionsRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}