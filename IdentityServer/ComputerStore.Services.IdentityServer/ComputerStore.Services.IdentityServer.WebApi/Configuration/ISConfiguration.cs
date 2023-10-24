using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace ComputerStore.Services.IdentityServer.WebApi.Configuration
{
    public static class ISConfiguration
    {
        public static IEnumerable<ApiScope> ApiScopes
            => new List<ApiScope>
            {
                new ApiScope("ComponentsApi", "Web Api")
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
                new ("ComponentsApi", "Web Api")
                {
                    Scopes = { "ComponentsApi" }
                }
            };

        public static IEnumerable<Client> Clients
            => new List<Client>
            {

                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = new [] { new Secret("client_secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "ComponentsApi" }
                }
               /* new Client
                {
                    ClientId = "client",
                    ClientName = "ClientApi", // wrong value
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
                        "ComponentsApi"
                    },
                    AllowAccessTokensViaBrowser = true,
                }*/
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
                    new ("given_name", "Mick"),
                    new ("family_name", "Mining")
                }
            },
            new TestUser
            {
                SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                Username = "Jane",
                Password = "JanePassword",
                Claims = new List<Claim>
                {
                    new ("given_name", "Jane"),
                    new ("family_name", "Downing")
                }
            }
        };
    }
}
