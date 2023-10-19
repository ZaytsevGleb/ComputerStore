using AutoMapper;
using ComputerStore.Services.Auth.BusinessLogic.Models;
using ComputerStore.Services.Auth.DataAccess.Entities;

namespace ComputerStore.Services.Auth.BusinessLogic.Mapper;
public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<UserModel, User>().ReverseMap();
    }
}
