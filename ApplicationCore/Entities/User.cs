using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class User : StorableEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public long UserTypeId { get; set; }
    public virtual UserType UserType { get; set; }
    public virtual List<Review> Reviews { get; init; } = new();
    public virtual List<Order> Orders { get; init; } = new();
    public DateTime? DeletionDateTime { get; set; }
}