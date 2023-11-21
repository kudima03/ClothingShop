using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Sections.Commands.Delete;

public class DeleteSectionCommandHandler(IRepository<Section> sectionsRepository) : IRequestHandler<DeleteSectionCommand, Unit>
{
    public async Task<Unit> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        Section? section = await sectionsRepository.GetFirstOrDefaultAsync
                               (predicate: x => x.Id == request.Id,
                                cancellationToken: cancellationToken);

        if (section is not null)
        {
            sectionsRepository.Delete(section);
            await sectionsRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}