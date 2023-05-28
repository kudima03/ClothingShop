using MediatR;

namespace DomainServices.Features.Templates.Commands.Delete;

public class DeleteCommand<TEntity> : IRequest<Unit>
{
    public DeleteCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}