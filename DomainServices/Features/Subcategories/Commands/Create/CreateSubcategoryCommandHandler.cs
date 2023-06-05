using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Subcategories.Commands.Create;

public class CreateSubcategoryCommandHandler : IRequestHandler<CreateSubcategoryCommand, Subcategory>
{
    private readonly IRepository<Subcategory> _repository;

    public CreateSubcategoryCommandHandler(IRepository<Subcategory> repository)
    {
        _repository = repository;
    }

    public async Task<Subcategory> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Subcategory? subcategory = await _repository.InsertAsync(request.Subcategory, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return subcategory;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException(
                $"Unable to perform create {nameof(Subcategory)} operation. Check input.");
        }
    }
}