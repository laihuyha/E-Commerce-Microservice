using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace KeyCloak.Auth.Extensions;

public static class AppServicesExtension
{
    internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI-Auth", Version = "v1" });

            var securitySchema = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(configuration["Keycloak:AuthorizationUrl"]!),
                        Scopes = new Dictionary<string, string>
                        {
                            {"openid", "openid"},
                            {"profile", "profile"},
                        }
                    }
                }
            };

            opt.AddSecurityDefinition("Keycloak", securitySchema);

            var securityRequirements = new OpenApiSecurityRequirement
            {
                {
                    new  OpenApiSecurityScheme{
                        Reference = new OpenApiReference { Id = "Keycloak", Type = ReferenceType.SecurityScheme },
                        In= ParameterLocation.Header,
                        Name = "Bearer",
                        Scheme = "Bearer"
                    }, []
                }
            };

            opt.AddSecurityRequirement(securityRequirements);
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.Audience = configuration["Authentication:Audience"];
            x.MetadataAddress = configuration["Authentication:MetadataAddress"]!;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["Authentication:ValidIssuer"],
            };
        });

        services.AddAuthorization();
        return services;
    }
}
