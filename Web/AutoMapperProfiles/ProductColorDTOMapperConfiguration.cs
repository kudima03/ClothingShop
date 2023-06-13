using ApplicationCore.Entities;
using AutoMapper;
using Web.DTOs;

namespace Web.AutoMapperProfiles;

public class ProductColorDTOMapperConfiguration : Profile
{
    public ProductColorDTOMapperConfiguration()
    {
        CreateMap<ProductColorDTO, ProductColor>();
    }
}
