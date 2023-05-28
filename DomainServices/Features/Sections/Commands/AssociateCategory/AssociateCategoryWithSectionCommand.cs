using MediatR;

namespace DomainServices.Features.Sections.Commands.AssociateCategory;

public class AssociateCategoryWithSectionCommand : IRequest<Unit>
{
    public AssociateCategoryWithSectionCommand(long categoryId, long sectionId)
    {
        SectionId = sectionId;
        CategoryId = categoryId;
    }

    public long CategoryId { get; init; }
    public long SectionId { get; init; }
}