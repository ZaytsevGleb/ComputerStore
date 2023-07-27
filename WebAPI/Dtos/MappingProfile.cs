using AutoMapper;
using BusinessLogic.Products.Models;

namespace WebAPI.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductModel, ProductDto>();
    }
}
