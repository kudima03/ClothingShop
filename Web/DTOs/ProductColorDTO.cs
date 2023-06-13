namespace Web.DTOs;

public class ProductColorDTO
{
    public long Id { get; set; }
    public string ColorHex { get; set; }
    public ICollection<ImageInfoDTO> ImagesInfos { get; init; } = new List<ImageInfoDTO>();
}
