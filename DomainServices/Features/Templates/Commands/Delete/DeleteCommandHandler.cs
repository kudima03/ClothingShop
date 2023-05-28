using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Templates.Commands.Delete;

public class DeleteCommandHandler<TEntity> : IRequestHandler<DeleteCommand<TEntity>, Unit> where TEntity : IStorable
{
    private readonly IRepository<TEntity> _repository;

    public DeleteCommandHandler(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteCommand<TEntity> request, CancellationToken cancellationToken)
    {
        TEntity? entityToDelete = await _repository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        if (entityToDelete is null)
        {
            return Unit.Value;
        }

        _repository.Delete(entityToDelete);
        await _repository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}