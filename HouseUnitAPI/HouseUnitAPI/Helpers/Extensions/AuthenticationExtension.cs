using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HouseUnitAPI.Helpers.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{configuration["Auth0:Domain"]}/";
                options.Audience = configuration["Auth0:Audience"];
                options.UseSecurityTokenValidators = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = $"https://{configuration["Auth0:Domain"]}/",
                    ValidAlgorithms = new[] { SecurityAlgorithms.RsaSha256 },
                };
                options.Events = new JwtBearerEvents
                {

                    OnTokenValidated = context =>
                    {
                        // Ensure roles are included in the claims
                        var token = context.SecurityToken as JwtSecurityToken;
                        var identity = (ClaimsIdentity)context.Principal.Identity;
                        var claims = identity.Claims;

                        // Add role claims from the Auth0 token
                        var roles = claims.Where(c => c.Type == "roles").Select(c => c.Value);
                        var permissions = token.Claims.Where(c => c.Type == "permissions").Select(c => c.Value);
                        foreach (var permission in permissions)
                        {
                            switch (permission)
                            {
                                case "read:items":
                                    identity.AddClaim(new Claim(ClaimTypes.Role, "USER"));
                                    break;
                                case "write:items":
                                    identity.AddClaim(new Claim(ClaimTypes.Role, "ADMIN"));
                                    break;
                                default: break;
                            }
                        }


                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine($"OnChallenge error: {context.AuthenticateFailure?.Message}");
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}
