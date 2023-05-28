using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DomainServices.Features.Templates.Commands.Update;

public class UpdateCommandHandler<TEntity> : IRequestHandler<UpdateCommand<TEntity>, Unit> where TEntity : IStorable
{
    private readonly IRepository<TEntity> _repository;

    public UpdateCommandHandler(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<Unit> Handle(UpdateCommand<TEntity> request, CancellationToken cancellationToken)
    {
        bool exists = await _repository.ExistsAsync(x => x.Id == request.Entity.Id,
            cancellationToken);
        if (!exists)
        {
            throw new ValidationException("Validation error",
                new[] { new ValidationFailure(nameof(request.Entity.Id), "Entity to update doesn't exists") });
        }

        _repository.Update(request.Entity);
        await _repository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}