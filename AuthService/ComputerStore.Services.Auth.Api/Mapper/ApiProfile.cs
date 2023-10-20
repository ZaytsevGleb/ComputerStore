using AutoMapper;
using ComputerStore.Services.Auth.Api.Dtos;
using ComputerStore.Services.Auth.BusinessLogic.Models;
using ComputerStore.Services.Auth.DataAccess.Entities;

namespace ComputerStore.Services.Auth.Api.Mapper;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<LoginDto, LoginModel>().ReverseMap();
        CreateMap<LoginResponseDto, LoginResponseModel>().ReverseMap();
        CreateMap<RegisterDto, RegisterModel>().ReverseMap();
        CreateMap<UserDto, UserModel>().ReverseMap();


        CreateMap<User, UserModel>().ReverseMap();
    }
}
