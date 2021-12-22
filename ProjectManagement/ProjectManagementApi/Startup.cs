using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Identity.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjectManagement.Core;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagementApi.Filters;
using ProjectManagementApi.Services;
using ProjectManagementApi.Configuration;
using ProjectManagementApi.Middleware;

namespace ProjectManagementApi
{
    public class Startup
    {
        readonly string AllowPolicy = "ProjectManagementPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddCustomAuthentication(Configuration);

            var settingsSectionAuth = Configuration.GetSection("Authentication");
            var authSettings = settingsSectionAuth.Get<AuthSettings>();

            var settingsSectionEmail = Configuration.GetSection("EmailProvider");
            var emailSettings = settingsSectionEmail.Get<EmailProviderSettings>();

            services.Configure<AuthSettings>(settingsSectionAuth);
            services.Configure<EmailProviderSettings>(settingsSectionEmail);

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddTransient<TokenManagerMiddleware>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddHttpContextAccessor();

            services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
                    .AddFluentValidation()
                    .AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowPolicy,
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectManagementAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert JWT token into field"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        System.Array.Empty<string>()
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectManagement-v1");
                    // Serve the Swagger UI at the app's root(http://localhost:<port>/)
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(AllowPolicy);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<TokenManagerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
