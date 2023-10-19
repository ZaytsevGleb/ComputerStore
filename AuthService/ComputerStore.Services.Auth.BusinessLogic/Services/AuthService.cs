using System.Text.Json.Serialization;
using AutoMapper;
using ComputerStore.Services.Auth.BusinessLogic.Abstractions;
using ComputerStore.Services.Auth.BusinessLogic.Errors;
using ComputerStore.Services.Auth.BusinessLogic.Models;
using ComputerStore.Services.Auth.DataAccess.Context;
using ComputerStore.Services.Auth.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace ComputerStore.Services.Auth.BusinessLogic.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public AuthService(
        UserManager<User> userManager, 
        ApplicationDbContext db,
        IMapper mapper)
    {
        _userManager = userManager;
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<UserModel> Register(RegisterModel model)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = model.Email,
            NormalizedEmail = model.Email.ToUpper(),
            NormalizedUserName = model.Name,
            UserName = model.Name
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            var errors = JsonConvert.SerializeObject(result.Errors);
            throw new BadRequestException($"Some errors during creation of user: {errors}");
        }

        return _mapper.Map<UserModel>(user);
    }

    public Task<LoginResponseModel> Login(LoginModel model)
    {
        throw new NotImplementedException();
    }
}
