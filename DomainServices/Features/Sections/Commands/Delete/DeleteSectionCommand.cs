using MediatR;

namespace DomainServices.Features.Sections.Commands.Delete;

public class DeleteSectionCommand : IRequest
{
    public DeleteSectionCommand(long id)
    {
        Id = id;
    }

    public long Id { get; init; }
}