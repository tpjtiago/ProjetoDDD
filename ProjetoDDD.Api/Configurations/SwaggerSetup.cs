using Microsoft.Extensions.DependencyInjection;
using NSwag;
using System;

namespace ProjetoDDD.Api.Configurations
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerDocument(config => 
            {
                config.AddSecurity("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.ApiKey
                });

                config.PostProcess = document => 
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "ProjetoDDD API";
                    document.Info.Description = "ProjetoDDD API";
                };
            });
        }
    }
}