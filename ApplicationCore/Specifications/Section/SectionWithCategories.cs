using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.Section;

public sealed class SectionWithCategories(Expression<Func<Entities.Section, bool>>? predicate = null)
    : Specification<Entities.Section, Entities.Section>(section => section,
                                                        predicate,
                                                        sections => sections.OrderBy(section => section.Name),
                                                        sections => sections.Include(section => section.Categories));