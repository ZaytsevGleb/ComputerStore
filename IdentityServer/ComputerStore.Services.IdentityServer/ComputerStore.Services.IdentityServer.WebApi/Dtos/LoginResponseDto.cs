using ComputerStore.Services.IdentityServer.WebApi.Common;

namespace ComputerStore.Services.IdentityServer.WebApi.Dtos;

public class LoginResponseDto
{
    public bool Succeeded { get; set; }
    public string AccessToken { get; set; }
    public FailureReason? FailureReason { get; set; }

    public static LoginResponseDto FailResult(FailureReason reason) => new()
    {
        Succeeded = false,
        FailureReason = reason
    };
}
