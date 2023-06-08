using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class Section : StorableEntity
{
    public string Name { get; set; }
    public virtual List<Category> Categories { get; init; } = new();
}