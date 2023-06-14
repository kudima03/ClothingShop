using ApplicationCore.Entities;
using AutoMapper;
using Web.DTOs;

namespace Web.AutoMapperProfiles;

public class ImageInfoMapperConfiguration : Profile
{
    public ImageInfoMapperConfiguration()
    {
        CreateMap<ImageInfoDto, ImageInfo>();
    }
}
