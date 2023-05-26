using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;

namespace DomainServices.Features.Categories.Commands.Delete;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator(IReadOnlyRepository<Category> categoriesRepository)
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Object must be not null");

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id cannot be less than zero.");

        RuleFor(x => x.Id)
            .MustAsync(async (brandId, cancellationToken) =>
                await categoriesRepository.ExistsAsync(x => x.Id == brandId, cancellationToken))
            .WithMessage(x => $"Category with id:{x.Id} doesn't exist.");
    }
}