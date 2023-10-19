using ComputerStore.Services.Auth.BusinessLogic.Abstractions;
using ComputerStore.Services.Auth.BusinessLogic.Models;

namespace ComputerStore.Services.Auth.BusinessLogic.Services;
internal class AuthService : IAuthService
{
    public Task<UserModel> Register(RegisterModel model)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponseModel> Login(LoginModel model)
    {
        throw new NotImplementedException();
    }
}
