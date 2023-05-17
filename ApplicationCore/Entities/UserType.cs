namespace ApplicationCore.Entities;

public class UserType
{
    public long Id { get; set; }
    public string Name { get; set; }
    public virtual List<User> Users { get; } = new();
}