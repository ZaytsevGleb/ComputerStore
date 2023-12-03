using ComputerStore.Services.IdentityServer.WebApi.Common;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace ComputerStore.Services.IdentityServer.WebApi.Configuration;

public static class ISConfiguration
{
    public static IEnumerable<ApiScope> ApiScopes
        => new List<ApiScope>
        {
            new ApiScope(ISConstants.ComponentsApiScope, "Web Api")
        };

    public static IEnumerable<IdentityResource> IdentityResources
        => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiResource> ApiResources
        => new List<ApiResource>
        {
            new ApiResource(ISConstants.ComponentsApiName, "Web Api", new[] {JwtClaimTypes.Name})
            {
                Scopes = { ISConstants.ComponentsApiScope }
            }
        };

    public static IEnumerable<Client> Clients
        => new List<Client>
        {
            new Client
            {
                ClientId = ISConstants.ClientId,
                ClientName = ISConstants.ClientName,
                ClientSecrets = new [] { new Secret(ISConstants.ClientSecret.Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { ISConstants.ComponentsApiScope }
            },
            new Client
            {
                ClientId = ISConstants.WebClientId,
                ClientName = ISConstants.WebClientName,
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris =
                {
                    "http://.../signin-oidc"
                },
                AllowedCorsOrigins =
                {
                    "http://..."
                },
                PostLogoutRedirectUris =
                {
                    "http:/.../signout-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    ISConstants.ComponentsApiScope
                },
                AllowAccessTokensViaBrowser = true,
            }
        };

    public static List<TestUser> TestUsers
    => new List<TestUser>
    {
        new TestUser
        {
            SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
            Username = "Mick",
            Password = "MickPassword",
            Claims = new List<Claim>
            {
                new Claim("given_name", "Mick"),
                new Claim("family_name", "Mining")
            }
        },
        new TestUser
        {
            SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
            Username = "Jane",
            Password = "JanePassword",
            Claims = new List<Claim>
            {
                new Claim("given_name", "Jane"),
                new Claim("family_name", "Downing")
            }
        }
    };
}
