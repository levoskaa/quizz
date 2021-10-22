using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.Configuration
{
    public static class Config
    {
        // Resources in the system
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("game", "Game Service"),
                new ApiScope("questions", "Questions Service"),
                new ApiScope("api-gateway", "Api Gateway"),
                new ApiScope("game.signalr-hub", "Game SignalR Hub"),
            };
        }

        // Clients wanting access to the resources
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // Angular Client
                new Client
                {
                    ClientId = "angular",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "game" }
                }
            };
        }
    }
}