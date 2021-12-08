﻿using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Quizz.Identity.Configuration
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

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "angular-web",
                    ClientName = "Quizz",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    RedirectUris = { "http://localhost:4200/signin-callback",
                                     "http://localhost:4200/assets/silent-callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:4200/signout-callback" },
                    AllowedCorsOrigins = { "http://localhost:4200" },

                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 600,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "game"
                    },
                }
            };
    }
}