using AutoMapper;
using BusinessLogic.Orders.Models;
using BusinessLogic.Products.Models;
using DataAccess.Entities;

namespace BusinessLogic.Common;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductModel>();
        CreateMap<Order, OrderModel>();
    }
}
