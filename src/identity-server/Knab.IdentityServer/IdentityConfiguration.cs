using IdentityServer4.Models;

namespace Knab.IdentityServer;

public sealed class IdentityConfiguration
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("quote-api.read")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource("OuoteApi")
            {
                Scopes = new List<string> {"quote-api.read"},
                ApiSecrets = new List<Secret> {new("supersecret".Sha256())}
            }
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client
            {
                ClientId = "quote-api-client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedScopes = {"quote-api.read"},
                AccessTokenLifetime = (int)TimeSpan.FromHours(6).TotalMilliseconds
            },
        };
}