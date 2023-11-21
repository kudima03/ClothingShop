using MediatR;

namespace DomainServices.Features.Sections.Commands.Delete;

public class DeleteSectionCommand(long id) : IRequest<Unit>
{
    public long Id { get; init; } = id;
}