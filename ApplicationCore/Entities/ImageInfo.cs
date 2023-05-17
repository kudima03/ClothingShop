namespace ApplicationCore.Entities;

public class ImageInfo
{
    public long Id { get; set; }
    public string Url { get; set; }
    public long ProductColorId { get; set; }
    public virtual ProductColor ProductColor { get; set; }
}