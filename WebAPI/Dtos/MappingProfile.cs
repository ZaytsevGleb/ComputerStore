using AutoMapper;
using BusinessLogic.Orders.Models;
using BusinessLogic.Products.Models;

namespace WebAPI.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductModel, ProductDto>();
            CreateMap<OrderModel, OrderDto>();
        }
    }
}
