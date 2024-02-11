using Microsoft.OpenApi.Models;

namespace ELM.Core.Presentation.Configuration.Extensions
{
    internal static class SwaggerExtensions
    {
        internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ELM Core API",
                    Version = "v1",
                    Description = "ELM Core API"
                });

                options.CustomSchemaIds(t => t.ToString());
            });

            return services;
        }

        internal static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ELM Core API");
            });

            return app;
        }
    }
}
