using ComputerStore.Services.Auth.BusinessLogic.Models;

namespace ComputerStore.Services.Auth.BusinessLogic.Abstractions;
public interface IAuthService
{
    Task<UserModel> Register(RegisterModel model, CancellationToken ct);
    Task<LoginResponseModel> Login(LoginModel model, CancellationToken ct);
    Task Logout(CancellationToken ct);
}
