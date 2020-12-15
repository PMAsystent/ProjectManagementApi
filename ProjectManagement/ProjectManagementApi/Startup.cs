using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectManagement.Core;
using ProjectManagementApi.Filters;


namespace ProjectManagementApi
{
    public class Startup
    {
        readonly string AllowPolicy = "MonopolyPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
                    .AddFluentValidation();


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //Develop comments for learning purpose:
            //Origin - Out base URL or URL'a used by our WebApi, For example: https://monopolyapi.net
            //Header - HTTP headers - used for passing additional information. CORS have main 10 headers, we can set many others headers, passing it to withheaders method
            //Method - HTTP methods like GET, POST etc. We can set what methods we can use
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowPolicy,
                    builder =>
                    {
                        builder.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(AllowPolicy);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
