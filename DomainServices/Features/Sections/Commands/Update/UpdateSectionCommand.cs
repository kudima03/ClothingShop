using MediatR;

namespace DomainServices.Features.Sections.Commands.Update;

public class UpdateSectionCommand : IRequest<Unit>
{
    public long Id { get; init; }
    public string Name { get; init; }
}