using ApplicationCore.Entities;
using AutoMapper;
using Web.DTOs;

namespace Web.AutoMapperProfiles;

public class ProductOptionDTOMapperConfiguration : Profile
{
    public ProductOptionDTOMapperConfiguration()
    {
        CreateMap<ProductOptionDto, ProductOption>();
    }
}
