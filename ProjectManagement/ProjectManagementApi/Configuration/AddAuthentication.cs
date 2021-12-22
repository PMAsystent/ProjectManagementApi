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
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
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
            var settingsSectionAuth = configuration.GetSection("Authentication");
            var authSettings = settingsSectionAuth.Get<AuthSettings>();

            var settingsSectionEmail = configuration.GetSection("EmailProvider");
            var emailSettings = settingsSectionEmail.Get<EmailProviderSettings>();

            services.Configure<AuthSettings>(settingsSectionAuth);
            services.Configure<EmailProviderSettings>(settingsSectionEmail);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

                    //SET ONLY IN-DEV TODO: make this automatic
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.AuthKey)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = authSettings.Issuer,
                        ValidAudience = authSettings.Audience,
                        ValidateLifetime = true
                    };
                });
        }
    }
}