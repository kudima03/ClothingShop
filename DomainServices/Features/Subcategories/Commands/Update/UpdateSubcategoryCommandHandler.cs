using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Subcategories.Commands.Update;

public class UpdateSubcategoryCommandHandler : IRequestHandler<UpdateSubcategoryCommand, Unit>
{
    private readonly IRepository<Subcategory> _repository;

    public UpdateSubcategoryCommandHandler(IRepository<Subcategory> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        Subcategory? subcategory = await _repository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == request.Subcategory.Id,
            cancellationToken: cancellationToken);

        if (subcategory is null)
        {
            throw new EntityNotFoundException($"{nameof(Subcategory)} with id:{request.Subcategory.Id} doesn't exist.");
        }

        subcategory.Name = request.Subcategory.Name;

        try
        {
            await _repository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException(
                $"Unable to perform update {nameof(Subcategory)} operation. Check input.");
        }

        return Unit.Value;
    }
}