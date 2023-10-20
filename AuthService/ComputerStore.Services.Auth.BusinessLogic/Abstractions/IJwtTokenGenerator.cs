using ComputerStore.Services.Auth.DataAccess.Entities;

namespace ComputerStore.Services.Auth.BusinessLogic.Abstractions;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
