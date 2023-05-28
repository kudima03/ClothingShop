#nullable enable
using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class CustomerInfo : IStorable
{
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public long Id { get; set; }
}