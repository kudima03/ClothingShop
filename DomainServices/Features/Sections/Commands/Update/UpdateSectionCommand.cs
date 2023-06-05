using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Sections.Commands.Update;

public class UpdateSectionCommand : IRequest<Unit>
{
    public UpdateSectionCommand(Section section)
    {
        Section = section;
    }

    public Section Section { get; init; }
}