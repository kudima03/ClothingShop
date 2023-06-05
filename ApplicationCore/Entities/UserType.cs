using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class UserType : IStorable
{
    public string Name { get; set; }
    public virtual List<User> Users { get; init; } = new();
    public long Id { get; set; }
}