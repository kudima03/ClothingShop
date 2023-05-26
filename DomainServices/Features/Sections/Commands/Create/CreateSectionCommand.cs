using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Sections.Commands.Create;

public class CreateSectionCommand : IRequest<Section>
{
    public CreateSectionCommand(Section section)
    {
        Section = section;
    }

    public Section Section { get; init; }
}