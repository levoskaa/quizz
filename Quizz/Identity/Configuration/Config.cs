using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.Configuration
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("game", "Game Service"),
                new ApiScope("questions", "Questions Service"),
                new ApiScope("api-gateway", "Api Gateway"),
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "angular",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "game" }
                }
            };
    }
}