using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Subcategories.Commands.Create;

public class CreateSubcategoryCommandHandler : IRequestHandler<CreateSubcategoryCommand, Subcategory>
{
    private readonly IRepository<Subcategory> _subcategoriesRepository;

    public CreateSubcategoryCommandHandler(IRepository<Subcategory> subcategoriesRepository)
    {
        _subcategoriesRepository = subcategoriesRepository;
    }

    public async Task<Subcategory> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        await ValidateSubcategoryNameAsync(request.Name, cancellationToken);

        Subcategory newSubcategory = new() { Name = request.Name };

        try
        {
            Subcategory? subcategory = await _subcategoriesRepository.InsertAsync(newSubcategory, cancellationToken);
            await _subcategoriesRepository.SaveChangesAsync(cancellationToken);
            return subcategory;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException(
                $"Unable to perform create {nameof(Subcategory)} operation. Check input.");
        }
    }

    private async Task ValidateSubcategoryNameAsync(string name, CancellationToken cancellationToken = default)
    {
        bool nameExists = await _subcategoriesRepository.ExistsAsync(x => x.Name == name, cancellationToken);

        if (nameExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("Subcategory.Name", "Such subcategory name already exists!")
            });
        }
    }
}