using AutoMapper;
using ComputerStore.Services.Auth.BusinessLogic.Abstractions;
using ComputerStore.Services.Auth.BusinessLogic.Errors;
using ComputerStore.Services.Auth.BusinessLogic.Models;
using ComputerStore.Services.Auth.DataAccess.Entities;
using ComputerStore.Services.Auth.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace ComputerStore.Services.Auth.BusinessLogic.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMapper _mapper;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _mapper = mapper;
    }

    public async Task<UserModel> Register(RegisterModel model, CancellationToken ct)
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

    public async Task<LoginResponseModel> Login(LoginModel model, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null)
        {
            return new LoginResponseModel()
            {
                Succeeded = false,
                FailureReason = FailureReason.UserNotFound
            };
        }

        if (!await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return new LoginResponseModel()
            {
                Succeeded = false,
                FailureReason = FailureReason.WrongPassword
            };
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

        if (!signInResult.Succeeded)
        {
            return new LoginResponseModel()
            {
                Succeeded = false,
                FailureReason = FailureReason.UnknownReason
            };
        }

        return new LoginResponseModel()
        {
            Succeeded = true,
            AccessToken = _jwtTokenGenerator.GenerateToken(user)
        };
    }

    public async Task Logout(CancellationToken ct)
    {
        await _signInManager.SignOutAsync();
    }
}
