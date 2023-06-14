using AutoMapper;
using DomainServices.Features.Products.Commands.Create;
using DomainServices.Features.Products.Commands.Update;
using Web.DTOs;

namespace Web.AutoMapperProfiles;

public class ProductDTOMapperConfiguration : Profile
{
    public ProductDTOMapperConfiguration()
    {
        CreateMap<ProductDto, CreateProductCommand>();
        CreateMap<ProductDto, UpdateProductCommand>();
    }
}
