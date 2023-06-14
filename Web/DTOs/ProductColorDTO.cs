namespace Web.DTOs;

public class ProductColorDto
{
    public long Id { get; set; }
    public string ColorHex { get; set; }
    public ICollection<ImageInfoDto> ImagesInfos { get; init; } = new List<ImageInfoDto>();
}
