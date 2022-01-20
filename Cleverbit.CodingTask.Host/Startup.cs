using AutoMapper;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Host.Auth;
using Cleverbit.CodingTask.Host.DependencyInjections;
using Cleverbit.CodingTask.Host.Middleware;
using Cleverbit.CodingTask.Services;
using Cleverbit.CodingTask.Utilities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace Cleverbit.CodingTask.Host
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
            .AddDbContext<CodingTaskContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IHashService>(new HashService(configuration.GetSection("HashSalt").Get<string>()));

            InjectCodingTaskDependencies.AddWebSiteDependencies(services);

            services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.ImplicitlyValidateChildProperties = false;
                    // Other way to register validators
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                }
              );

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
                });
            }

            //Add Custom Exception middlerware implementation.
            app.UseExceptionMiddleware();


            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "Views")),
                RequestPath = "/Views"
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
