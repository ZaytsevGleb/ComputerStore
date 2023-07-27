using AutoMapper;
using BusinessLogic.Products.Models;
using DataAccess.Entities;

namespace BusinessLogic.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductModel>();
    }
}
