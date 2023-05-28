using ApplicationCore.Entities.Interfaces;
using MediatR;

namespace DomainServices.Features.Templates.Commands.Update;

public class UpdateCommand<TEntity> : IRequest<Unit> where TEntity : IStorable
{
    public UpdateCommand(TEntity entity)
    {
        Entity = entity;
    }

    public TEntity Entity { get; set; }
}