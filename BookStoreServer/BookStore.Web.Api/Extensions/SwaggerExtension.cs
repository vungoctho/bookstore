using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BookStore.Web.Api.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerService(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BookStore API", Version = "v1" });
                // get xml comments path
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                // set xml path
                options.IncludeXmlComments(xmlPath);

                // set API key
                options.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Please enter your ApiKey",
                    Name = "ApiKey",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "ApiKey"
                });
                var secReq = new Microsoft.OpenApi.Models.OpenApiSecurityRequirement();
                secReq.Add(new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference()
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    Name = "ApiKey",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Scheme = "ApiKey"
                }, new List<string>());
                options.AddSecurityRequirement(secReq);
            });
        }

        public static void UseSwaggerService(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, ect...)
            // specifying the Swagger JSON endpoint
            app.UseSwaggerUI(act =>
            {
                act.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "BookStore API v1");
                act.DocumentTitle = "Title Documentation";
                act.DocExpansion(DocExpansion.None);
            });
        }

    }
}
