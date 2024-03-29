﻿using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Subcategories.Commands.Create;

public class CreateSubcategoryCommandHandler(IRepository<Subcategory> subcategoriesRepository,
                                             IRepository<Category> categoriesRepository)
    : IRequestHandler<CreateSubcategoryCommand, Subcategory>
{
    public async Task<Subcategory> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        await ValidateSubcategoryNameAsync(request.Name, cancellationToken);

        List<Category> categories = await ValidateAndGetCategoriesAsync(request.CategoriesIds, cancellationToken);

        Subcategory newSubcategory = new Subcategory
        {
            Name = request.Name,
            Categories = categories
        };

        try
        {
            Subcategory? subcategory = await subcategoriesRepository.InsertAsync(newSubcategory, cancellationToken);
            await subcategoriesRepository.SaveChangesAsync(cancellationToken);

            return subcategory;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Subcategory)} operation. Check input.");
        }
    }

    private async Task ValidateSubcategoryNameAsync(string name, CancellationToken cancellationToken = default)
    {
        bool nameExists = await subcategoriesRepository.ExistsAsync(x => x.Name == name, cancellationToken);

        if (nameExists)
        {
            throw new ValidationException
                (new[]
                {
                    new ValidationFailure("Subcategory.Name", "Such subcategory name already exists!")
                });
        }
    }

    private async Task<List<Category>> ValidateAndGetCategoriesAsync(ICollection<long> categoriesIds,
                                                                     CancellationToken cancellationToken = default)
    {
        IList<Category>? existingCategories = await categoriesRepository
                                                  .GetAllAsync
                                                      (predicate: x => categoriesIds.Contains(x.Id),
                                                       cancellationToken: cancellationToken);

        if (existingCategories.Count != categoriesIds.Count)
        {
            IEnumerable<long> missingCategoriesIds = categoriesIds.Except(existingCategories.Select(x => x.Id));
            string missingCategoriesMessage = string.Join(',', missingCategoriesIds);

            throw new EntityNotFoundException($"Categories with ids:{missingCategoriesMessage} doesn't exist.");
        }

        return existingCategories.ToList();
    }
}