using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class Brand : StorableEntity
{
    public string Name { get; set; }

    public virtual List<Product> Products { get; init; } = new List<Product>();
}