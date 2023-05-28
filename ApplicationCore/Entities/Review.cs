#nullable enable
using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Review : IStorable
{
    public int Rate { get; set; }
    public DateTime DateTime { get; set; }
    public string? Comment { get; set; }
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public long ProductId { get; set; }
    public virtual Product Product { get; set; }
    public long Id { get; set; }
}