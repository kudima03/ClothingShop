using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Section;

public sealed class SectionWithCategories : Specification<Entities.Section, Entities.Section>
{
    public SectionWithCategories(Expression<Func<Entities.Section, bool>>? predicate = null)
        : base(section => section,
            predicate,
            sections => sections.OrderBy(section => section.Name),
            sections => sections.Include(section => section.Categories))
    {
    }
}