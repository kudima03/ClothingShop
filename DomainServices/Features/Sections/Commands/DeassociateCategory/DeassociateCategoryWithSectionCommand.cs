using MediatR;

namespace DomainServices.Features.Sections.Commands.DeassociateCategory;

public class DeassociateCategoryWithSectionCommand : IRequest<Unit>
{
    public DeassociateCategoryWithSectionCommand(long categoryId, long sectionId)
    {
        CategoryId = categoryId;
        SectionId = sectionId;
    }

    public long SectionId { get; init; }
    public long CategoryId { get; init; }
}