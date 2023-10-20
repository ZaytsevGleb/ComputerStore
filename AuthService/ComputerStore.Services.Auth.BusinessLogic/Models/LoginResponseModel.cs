using ComputerStore.Services.Auth.Shared.Enums;

namespace ComputerStore.Services.Auth.BusinessLogic.Models;
public class LoginResponseModel
{
    public bool Succeeded { get; set; }
    public string AccessToken { get; set; }
    public FailureReason? FailureReason { get; set; }

    public static LoginResponseModel FailResult(FailureReason reason) => new()
    {
        Succeeded = false,
        FailureReason = reason
    };
}
