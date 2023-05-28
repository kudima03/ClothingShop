using ApplicationCore.Entities.Interfaces;
using MediatR;

namespace DomainServices.Features.Templates.Commands.Create;

public class CreateCommand<TEntity> : IRequest<TEntity> where TEntity : IStorable
{
    public CreateCommand(TEntity entity)
    {
        Entity = entity;
    }

    public TEntity Entity { get; set; }
}