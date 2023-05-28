using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Specifications.Section;

public sealed class GetSectionsWithCategories : Specification<Entities.Section, Entities.Section>
{
    public GetSectionsWithCategories()
        : base(section => section,
            orderBy: sections => sections.OrderBy(section => section.Id),
            include: sections => sections.Include(section => section.Categories))
    {
    }
}