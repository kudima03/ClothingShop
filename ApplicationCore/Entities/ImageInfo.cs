using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class ImageInfo : StorableEntity
{
    public string Url { get; set; }
    public long ProductColorId { get; set; }
    public virtual ProductColor ProductColor { get; set; }
}