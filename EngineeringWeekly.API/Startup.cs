using EngineeringWeekly.API.Logging;
using EngineeringWeekly.API.Componets;
using EngineeringWeekly.API.Componets.Interfaces;
using EngineeringWeekly.DTOS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CodeTest
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Configuration["ServiceConfig:OS"] = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            Configuration["ServiceConfig:Environment"] = env.EnvironmentName;
            Configuration["ServiceConfig:ServiceVersion"] = Environment.GetEnvironmentVariable("BUILD_VERSION") ?? "1.0.0.0";
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddMvc();
            services.Configure<ServiceConfigOptions>(Configuration.GetSection("ServiceConfig"));
            services.Configure<SwaggerInfo>(Configuration.GetSection("SwaggerInfo"));
            services.Configure<ExternalAPIUrs>(Configuration.GetSection("ExternalAPIUrs"));

            
            var swaggerInfo = Configuration.GetSection("SwaggerInfo").Get<SwaggerInfo>();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc(swaggerInfo.Version, new OpenApiInfo { Title = swaggerInfo.Description, Version = swaggerInfo.Version });
            });

            services.AddScoped<IChuckNorrisJokeGetter, ChuckNorrisJokeGetter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddProvider(new TextFileLoggerProvider(configuration));

            var swaggerInfo = Configuration.GetSection("SwaggerInfo").Get<SwaggerInfo>();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{swaggerInfo.Version}/swagger.json", $"{swaggerInfo.Description} {swaggerInfo.Version}"));

            app.UseCors(builder =>
                builder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
