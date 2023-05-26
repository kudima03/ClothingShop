using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Sections.Commands.Create;

public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Section>
{
    private readonly IRepository<Section> _sectionsRepository;

    public CreateSectionCommandHandler(IRepository<Section> sectionsRepository)
    {
        _sectionsRepository = sectionsRepository;
    }

    public async Task<Section> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        Section? entity = await _sectionsRepository.InsertAsync(request.Section, cancellationToken);
        await _sectionsRepository.SaveChangesAsync(cancellationToken);
        return entity;
    }
}