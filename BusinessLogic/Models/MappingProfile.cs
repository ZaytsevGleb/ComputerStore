using AutoMapper;
using DataAccess.Entities;

namespace BusinessLogic.Models;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductModel>().ReverseMap();
    }
}
