using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class ImageInfo : IStorable
{
    public string Url { get; set; }
    public long ProductColorId { get; set; }
    public virtual ProductColor ProductColor { get; set; }
    public long Id { get; set; }
}