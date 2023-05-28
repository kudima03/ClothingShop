using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Templates.Commands.Create;

public class CreateCommandHandler<TEntity> : IRequestHandler<CreateCommand<TEntity>, TEntity> where TEntity : IStorable
{
    protected readonly IRepository<TEntity> Repository;

    public CreateCommandHandler(IRepository<TEntity> repository)
    {
        Repository = repository;
    }

    public virtual async Task<TEntity> Handle(CreateCommand<TEntity> request, CancellationToken cancellationToken)
    {
        TEntity? entity = await Repository.InsertAsync(request.Entity, cancellationToken);
        await Repository.SaveChangesAsync(cancellationToken);
        return entity;
    }
}