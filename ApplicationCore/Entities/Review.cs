#nullable enable
namespace ApplicationCore.Entities;

public class Review
{
    public long Id { get; set; }
    public int Rate { get; set; }
    public DateTime DateTime { get; set; }
    public string? Comment { get; set; }
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public long ProductId { get; set; }
    public virtual Product Product { get; set; }
}