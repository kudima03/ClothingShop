using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class User : IStorable
{
    public string Email { get; set; }
    public string Password { get; set; }
    public long UserTypeId { get; set; }
    public virtual UserType UserType { get; set; }
    public virtual List<Review> Reviews { get; } = new();
    public virtual List<Order> Orders { get; } = new();
    public long Id { get; set; }
}