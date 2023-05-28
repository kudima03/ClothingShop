using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Sections.Commands.AssociateCategory;

public class AssociateCategoryWithSectionCommandHandler
    : IRequestHandler<AssociateCategoryWithSectionCommand, Unit>
{
    private readonly IRepository<Category> _categoriesRepository;
    private readonly IRepository<Section> _sectionsRepository;

    public AssociateCategoryWithSectionCommandHandler(IRepository<Category> categoriesRepository,
        IRepository<Section> sectionsRepository)
    {
        _categoriesRepository = categoriesRepository;
        _sectionsRepository = sectionsRepository;
    }

    public async Task<Unit> Handle(AssociateCategoryWithSectionCommand request,
        CancellationToken cancellationToken)
    {
        (Section section, Category category) =
            await ValidateAndGetValues(request.SectionId, request.CategoryId, cancellationToken);

        section.Categories.Add(category);

        await _sectionsRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<(Section section, Category category)> ValidateAndGetValues(long sectionId, long categoryId,
        CancellationToken cancellationToken = default)
    {
        Section? section = await _sectionsRepository
            .GetFirstOrDefaultAsync(x => x.Id == sectionId,
                sections => sections.Include(sec => sec.Categories),
                cancellationToken);

        if (section is null)
        {
            throw new ValidationException("Validation error.",
                new[]
                {
                    new ValidationFailure($"{nameof(section.Id)}",
                        $"Section with id:{sectionId} not found")
                });
        }

        Category? category = await _categoriesRepository
            .GetFirstOrDefaultAsync(predicate: x => x.Id == categoryId,
                cancellationToken: cancellationToken);
        if (category is null)
        {
            throw new ValidationException("Validation error.",
                new[]
                {
                    new ValidationFailure($"{nameof(category.Id)}",
                        $"Category with id:{categoryId} not found")
                });
        }

        return (section, category);
    }
}