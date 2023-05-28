using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Specifications.Section;

public sealed class GetSectionByIdWithCategories : Specification<Entities.Section, Entities.Section>
{
    public GetSectionByIdWithCategories(long id)
        : base(section => section,
            section => section.Id == id,
            sections => sections.OrderBy(section => section.Id),
            sections => sections.Include(section => section.Categories))
    {
    }
}