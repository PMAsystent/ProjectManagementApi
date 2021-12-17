using System.Text;
using Infrastructure.Identity.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ProjectManagementApi.Configuration
{
    public static class AddCustomAuth
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("Testing"))
            {
                AddTestingAuthentication(services);
            }
            else
            {
                AddApplicationAuthentication(services, configuration);
            }
        }

        private static void AddTestingAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Integration tests";
                options.DefaultChallengeScheme = "Integration tests";
            }).AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                "Integration tests", options => { });
        }

        private static void AddApplicationAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var settingsSection = configuration.GetSection("Authentication");
            var settings = settingsSection.Get<AppSettings>();
            services.Configure<AppSettings>(settingsSection);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    // IdentityServer emits a typ header by default, recommended extra check
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

                    //SET ONLY IN-DEV TODO: make this automatic
                    options.RequireHttpsMetadata = false;

                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.AuthKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
        }
    }
}