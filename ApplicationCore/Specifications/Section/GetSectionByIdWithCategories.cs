using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Section;

public sealed class GetSectionByIdWithCategories : ISpecification<Entities.Section, Entities.Section>
{
    public GetSectionByIdWithCategories(long id)
    {
        Selector = section => section;
        Predicate = section => section.Id == id;
        OrderBy = sections => sections.OrderBy(section => section.Id);
        Include = sections => sections.Include(section => section.Categories);
    }

    public Expression<Func<Entities.Section, Entities.Section>> Selector { get; init; }
    public Expression<Func<Entities.Section, bool>> Predicate { get; init; }
    public Func<IQueryable<Entities.Section>, IOrderedQueryable<Entities.Section>> OrderBy { get; init; }
    public Func<IQueryable<Entities.Section>, IIncludableQueryable<Entities.Section, object>> Include { get; init; }
}