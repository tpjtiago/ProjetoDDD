using HealthChecks.UI.Client;
using Crosscutting.Domain.Authentication;
using Crosscutting.Domain.ErrorHandler;
using Crosscutting.Infrastructure.Contexts.MongoDb;
using Crosscutting.Infrastructure.Contexts.Postgre;
using Crosscutting.Infrastructure.Contexts.Redis;
using ProjetoDDD.Api.Configurations;
using ProjetoDDD.Infrastructure.Contexts;
using ProjetoDDD.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ProjetoDDD.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIrisAuthentication(Configuration);
            services.AddAutoMapperSetup();
            services.AddSwaggerSetup();
            services.AddPostgreContext<ProjetoDDDPostgreContext>(Configuration);
            services.AddMongoDbContext(Configuration);
            services.AddEventStoreContext(Configuration);
            services.AddRedisContext(Configuration);

            services.AddHealthChecks()
                .AddMongoDb(Configuration.GetSection("MongoDb:ConnectionString").Value, name: "MongoDb Connection")
                .AddMongoDb(Configuration.GetSection("MongoDb:EventStoreConnectionString").Value, name: "EventStore Connection")
                .AddRedis(Configuration.GetSection("Redis:ConnectionString").Value, name: "Redis Connection")
                .AddNpgSql(Configuration.GetSection("Postgre:ConnectionString").Value, name: "Postgre Connection");

            services.AddHealthChecksUI();
            services.AddControllers();
            services.AddMediatR(typeof(Startup));
            services.AddApiVersioning(v =>
            {
                v.AssumeDefaultVersionWhenUnspecified = true;
                v.DefaultApiVersion = new ApiVersion(DateTime.Now);
            });

            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.ConfigureExceptionHandler();

            app.UseHealthChecks("/healthchecks", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI();
            app.UseOpenApi();
            app.UseSwaggerUi3(s =>
            {
                s.Path = "/projetoddd-api/swagger";
            });

            app.UseApiVersioning();
            app.UseRouting();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services, Configuration);
        }
    }
}