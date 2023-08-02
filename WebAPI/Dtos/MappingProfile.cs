using AutoMapper;
using BusinessLogic.Models;

namespace WebAPI.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductModel, ProductDto>().ReverseMap();
    }
}
