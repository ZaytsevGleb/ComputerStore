namespace ComputerStore.Services.Auth.BusinessLogic.Infrastructure.Options;

public class JwtOptions
{
    public string Secret { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int ExpireMinutes { get; set; }
}
