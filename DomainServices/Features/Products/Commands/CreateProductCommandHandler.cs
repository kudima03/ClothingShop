using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Features.Templates.Commands.Create;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.Products.Commands;
public class CreateProductCommandHandler : CreateCommandHandler<Product>
{
    private readonly IRepository<Brand> _brandsRepository;

    private readonly IRepository<Subcategory> _subcategoriesRepository;
    public CreateProductCommandHandler(IRepository<Product> repository,
        IRepository<Subcategory> subcategoriesRepository,
        IRepository<Brand> brandsRepository) : base(repository)
    {
        _subcategoriesRepository = subcategoriesRepository;
        _brandsRepository = brandsRepository;
    }

    private async Task ValidateRelationsAsync(Product productToAdd)
    {
        if (!await _brandsRepository.ExistsAsync(selector: x => x.Id == productToAdd.BrandId))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(productToAdd.BrandId)}",
                    $"{productToAdd.Brand.GetType().Name} with id: {productToAdd.BrandId} doesn't exist")
            });
        }

        if (! await _subcategoriesRepository.ExistsAsync(selector: x=>x.Id == productToAdd.SubcategoryId))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure($"{nameof(productToAdd.SubcategoryId)}",
                    $"{productToAdd.Subcategory.GetType().Name} with id: {productToAdd.SubcategoryId} doesn't exist")
            });
        }
    }

    public override async Task<Product> Handle(CreateCommand<Product> request, CancellationToken cancellationToken)
    {
        await ValidateRelationsAsync(request.Entity);
        return await base.Handle(request, cancellationToken);
    }
}
