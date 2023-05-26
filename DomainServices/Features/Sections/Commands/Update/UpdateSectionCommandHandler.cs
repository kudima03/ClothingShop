using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Sections.Commands.Update;

public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand>
{
    private readonly IRepository<Section> _sectionsRepository;

    public UpdateSectionCommandHandler(IRepository<Section> sectionsRepository)
    {
        _sectionsRepository = sectionsRepository;
    }

    public async Task Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        _sectionsRepository.Update(request.Section);
        await _sectionsRepository.SaveChangesAsync(cancellationToken);
    }
}