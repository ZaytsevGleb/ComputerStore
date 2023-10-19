namespace ComputerStore.Services.Auth.Api.Dtos;

public class LoginResponseDto
{
    public UserDto User { get; set; }
    public string Token { get; set; }
}
