using AutoMapper;
using BookStore.RedisMessageQueue.DI;
using BookStore.Web.Api.ActionFilters;
using BookStore.Web.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace BookStore.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IServiceProvider ServiceProvider { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services.AddMvc(c =>
            {
                c.Filters.Add(new ApiKeyActionFilter(Configuration));
                c.Filters.Add(new GlobalExceptionFilter(ServiceProvider));
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerService();
            services.AddAutoMapper(typeof(Startup));
            var redisServerEndpoint = Configuration.GetValue<string>("Redis:Endpoint");
            services.AddRedisMQType(redisServerEndpoint);

            ServiceProvider = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/log-{Date}.txt");
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseRouting();
            app.UseCors(opts =>
            {
                opts
                    .SetIsOriginAllowed(origin => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                ;
            });

            app.UseSwaggerService();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
