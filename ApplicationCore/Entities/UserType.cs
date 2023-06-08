using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class UserType : StorableEntity
{
    public string Name { get; set; }
    public virtual List<User> Users { get; init; } = new();
}