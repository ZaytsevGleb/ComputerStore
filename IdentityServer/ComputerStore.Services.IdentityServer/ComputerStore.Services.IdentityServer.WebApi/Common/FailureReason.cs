namespace ComputerStore.Services.IdentityServer.WebApi.Common
{
    public enum FailureReason
    {
        None = 0,
        UserNotFound = 1,
        WrongPassword = 2,
        UnknownReason = 3,
        ExistingUser = 4
    }
}
