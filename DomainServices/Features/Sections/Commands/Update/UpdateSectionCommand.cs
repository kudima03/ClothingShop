using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.Sections.Commands.Update;

public class UpdateSectionCommand : IRequest
{
    public UpdateSectionCommand(Section section)
    {
        Section = section;
    }

    public Section Section { get; init; }
}