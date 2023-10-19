using AutoMapper;
using ComputerStore.Services.CS.DataAccess.Entities;

namespace ComputerStore.Services.CS.BusinessLogic.Models;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductModel>().ReverseMap();
    }
}
